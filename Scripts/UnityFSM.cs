using System;
using System.Collections.Generic;

namespace UnityFSM
{
    /// <summary>Generic value holder.</summary>
    public class Value<T>
    {
        public T value;
    }

    public enum ParameterType
    {
        Int,
        Float,
        Bool,
        Trigger
    };

    /// <summary>Named FSM parameter holding a bool, int, float or trigger value.</summary>
    [Serializable]
    public class Parameter
    {
        // Reading a trigger consumes it: true once, then false until it is set again
        public bool triggerValue
        {
            get
            {
                if (tValue)
                {
                    tValue = false;
                    return true;
                }
                else
                    return false;
            }
            set { tValue = value; }
        }

        public string name;
        public ParameterType type;

        public bool boolValue;
        public int intValue;
        public float floatValue;

        private bool tValue;


        public Parameter(ParameterType type, string name)
        {
            this.type = type;
            this.name = name;
        }

        public void SetValue(bool v)
        {
            boolValue = v;
        }

        public void SetValue(int v)
        {
            intValue = v;
        }

        public void SetValue(float v)
        {
            floatValue = v;
        }

        public void SetValue()
        {
            triggerValue = !triggerValue;
        }
    }

    /// <summary>Moves the FSM to a destination state when all its conditions are met.</summary>
    [Serializable]
    public class Transition
    {
        public string name;
        public State destination;

        public List<Condition> conditions = new List<Condition>();


        public Transition(string name)
        {
            this.name = name;
        }

        // All conditions must be true; a transition without conditions never fires
        public bool CheckTransition(UFSM fsm)
        {
            bool result = false;

            for (int i = 0; i < conditions.Count; i++)
            {
                result = conditions[i].IsTrue(fsm);

                if (result == false)
                    break;
            }

            return result;
        }

        public void AddCondition(string parameterName)
        {
            conditions.Add(new Condition(parameterName));
        }

        public void DeleteCondition(Condition condition)
        {
            conditions.Remove(condition);
        }

        public void DeleteAll()
        {
            conditions.Clear();
        }
    }

    /// <summary>Compares a parameter against a criteria; the fields used depend on the parameter type.</summary>
    [Serializable]
    public class Condition
    {
        public enum BoolCriteria
        {
            IsTrue,
            IsFalse
        }

        public enum IntCriteria
        {
            Equal,
            LessThan,
            MoreThan
        }

        public enum FloatCriteria
        {
            Equal,
            LessThan,
            MoreThan
        }

        public string selectedParameterName;

        public BoolCriteria boolCriteria;

        public IntCriteria intCriteria;
        public int intValueComparision;

        public FloatCriteria floatCriteria;
        public float floatValueComparision;


        public Condition(string parameterName)
        {
            selectedParameterName = parameterName;
        }

        public bool IsTrue(UFSM fsm)
        {
            bool result = false;

            Parameter selectedParameter = null;

            for (int i = 0; i < fsm.parameters.Count; i++)
            {
                if (fsm.parameters[i].name == selectedParameterName)
                {
                    selectedParameter = fsm.parameters[i];
                    break;
                }
            }

            // A missing parameter has no type and falls into default (false)
            switch (selectedParameter?.type)
            {
                #region BOOL
                case ParameterType.Bool:
                    switch (boolCriteria)
                    {
                        case BoolCriteria.IsTrue:
                            if (selectedParameter.boolValue)
                                result = true;
                            break;
                        case BoolCriteria.IsFalse:
                            if (!selectedParameter.boolValue)
                                result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
                #endregion
                #region INT
                case ParameterType.Int:
                    switch (intCriteria)
                    {
                        case IntCriteria.Equal:
                            if (selectedParameter.intValue == intValueComparision)
                                result = true;
                            break;
                        case IntCriteria.LessThan:
                            if (selectedParameter.intValue < intValueComparision)
                                result = true;
                            break;
                        case IntCriteria.MoreThan:
                            if (selectedParameter.intValue > intValueComparision)
                                result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
                #endregion
                #region FLOAT
                case ParameterType.Float:
                    switch (floatCriteria)
                    {
                        case FloatCriteria.Equal:
                            if (selectedParameter.floatValue == floatValueComparision)
                                result = true;
                            break;
                        case FloatCriteria.LessThan:
                            if (selectedParameter.floatValue < floatValueComparision)
                                result = true;
                            break;
                        case FloatCriteria.MoreThan:
                            if (selectedParameter.floatValue > floatValueComparision)
                                result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                    break;
                #endregion
                #region TRIGGER
                case ParameterType.Trigger:
                    if (selectedParameter.triggerValue)
                        result = true;
                    break;
                #endregion
                default:
                    result = false;
                    break;
            }

            return result;
        }
    }
}
