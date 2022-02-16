using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AvoidContact.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;
        public virtual float Value
        {
            get
            {
                if (_isModified || BaseValue != _lastBaseValue)
                {
                    _lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    _isModified = false;
                }
                return _value;
            }
        }
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        protected float _value;
        protected float _lastBaseValue = float.MinValue;
        protected bool _isModified = true;
        protected readonly List<StatModifier> _statModifiers;

        public CharacterStat()
        {
            _statModifiers = new List<StatModifier>();
            StatModifiers = _statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier modifier)
        {
            _isModified = true;
            _statModifiers.Add(modifier);
            _statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            bool didRemove = false;
            if (_statModifiers.Remove(modifier))
            {
                _isModified = true;
                didRemove = true;
            }
            return didRemove;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = _statModifiers.Count - 1; i < 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    _isModified = true;
                    _statModifiers.RemoveAt(i);
                    didRemove = true;
                }
            }
            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }
            return 0;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier modifier = _statModifiers[i];

                switch (modifier.Type)
                {
                    case StatModifierType.Flat:
                        finalValue += modifier.Value;
                        break;
                    case StatModifierType.PercentAdd:
                        sumPercentAdd += modifier.Value;
                        if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                        break;
                    case StatModifierType.PercentMultiply:
                        finalValue *= 1 + modifier.Value;
                        break;
                    default:
                        break;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }
    }
}