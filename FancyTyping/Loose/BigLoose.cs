using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ExtendedTyping
{
    public class BigLoose : ILoose
    {
        #region public Properties
        public ReadOnlySetWrapper<Type> WhiteListSet => whiteList;
        public IEnumerable<Type> WhiteList => whiteList.ToITypeArray();

        /// <summary>
        /// The value stored in the current Loose type.
        /// </summary>
        public dynamic V
        {
            get => value;
            set
            {
                if (whiteList.Contains(value.GetType()))
                {
                    this.value = value;
                }
                else
                {
                    throw new ArgumentException($"Cannot set Loose.V to value of type {value.GetType()} because it was not be found in Loose.WhiteList");
                }
            }
        }
        /// <summary>
        /// The type of the value stored in the current Loose type.
        /// </summary>
        public Type Type => value.GetType();
        /// <summary>
        /// The value stored in the current Loose type in the form of an object.
        /// </summary>
        public object O => value;
        #endregion

        #region public Constructors
        private BigLoose() { }
        public BigLoose(IEnumerable<Type> whiteList)
        {
            this.whiteList = new HashSet<Type>(whiteList);
        }
        #endregion

        #region public Methods
        /// <summary>
        /// Creates and returns a new BigLoose with the same white list (borrowed) as the current BigLoose and a shallow copy of it's value.
        /// </summary>
        /// <returns></returns>
        public BigLoose BorrowClone()
        {
            BigLoose result = new BigLoose();
            result.whiteList = whiteList;
            result.value = value;
            return result;
        }

        /// <summary>
        /// Creates and returns a new BigLoose with a copy of the white list from the current BigLoose and a shallow copy of it's value.
        /// </summary>
        /// <returns></returns>
        public BigLoose Clone()
        {
            BigLoose result = new BigLoose();
            result.whiteList = new HashSet<Type>(whiteList);
            result.value = value;
            return result;
        }
        public override string ToString() => value.ToString();
        public override bool Equals(object obj) => value.Equals(obj);
        public override int GetHashCode() => value.GetHashCode();

        /// <summary>
        /// Checks if the given type is in the white list and would be allowed in Loose.V. If false is returned, Trying to set Loose.V to the given type would cause an exception to be thrown.
        /// </summary>
        /// <param name="t">The type to be checked.</param>
        /// <returns>True if the type was found in the white list and false otherwise.</returns>
        public bool CheckType(Type t)
        {
            foreach(Type type in t.GetSelfAndParents())
            {
                if (whiteList.Contains(type)) return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to set V to the value given.
        /// </summary>
        /// <param name="v">Value given.</param>
        /// <returns>True if success; False otherwise.</returns>
        public bool TrySetV(dynamic v)
        {
            if (CheckType(v.GetType()))
            {
                V = v;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to add the given type to the white list. Note that types cannot be removed from the white list.
        /// </summary>
        /// <param name="t">The type to be added.</param>
        /// <returns>True if the type was successfully added; false otherwise.</returns>
        public bool AddType(Type t) => whiteList.Add(t);

        /// <summary>
        /// Tries to remove the given type from the white list. If successful and the value is no longer valid, the value will be reset to default to prevent an invalid type from being stored.
        /// </summary>
        /// <param name="t">The type to remove.</param>
        /// <returns>True if the type was found in the white list; false otherwise.</returns>
        public bool RemoveType(Type t)
        {
            if (whiteList.Remove(t))
            {
                if (CheckType(value.GetType())) value = default;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region public static Methods
        #region Operators
        public static bool operator ==(BigLoose left, BigLoose right) => left.value.Equals(right.value);
        public static bool operator ==(BigLoose left, object right) => left.value.Equals(right);
        public static bool operator ==(object left, BigLoose right) => left.Equals(right.value);
        public static bool operator !=(BigLoose left, BigLoose right) => !left.value.Equals(right.value);
        public static bool operator !=(BigLoose left, object right) => !left.value.Equals(right);
        public static bool operator !=(object left, BigLoose right) => !left.Equals(right.value);
        #endregion
        #endregion

        #region private Fields
        private dynamic value;
        private HashSet<Type> whiteList;
        #endregion
    }
}
