﻿using System.Collections.ObjectModel;
using PRoCon.Core.Remote;

namespace PRoCon.Core.Accounts {
    public class AccountPrivilegeDictionary : KeyedCollection<string, AccountPrivilege> {
        public delegate void AccountPrivilegeAlteredHandler(AccountPrivilege item);

        public event AccountPrivilegeAlteredHandler AccountPrivilegeAdded;
        public event AccountPrivilegeAlteredHandler AccountPrivilegeChanged;
        public event AccountPrivilegeAlteredHandler AccountPrivilegeRemoved;

        protected override string GetKeyForItem(AccountPrivilege item) {
            return item.Owner.Name;
        }

        protected override void InsertItem(int index, AccountPrivilege item) {
            base.InsertItem(index, item);

            if (AccountPrivilegeAdded != null) {
                FrostbiteConnection.RaiseEvent(AccountPrivilegeAdded.GetInvocationList(), item);
            }
        }

        protected override void RemoveItem(int index) {
            AccountPrivilege apRemoved = this[index];

            base.RemoveItem(index);

            if (AccountPrivilegeRemoved != null) {
                FrostbiteConnection.RaiseEvent(AccountPrivilegeRemoved.GetInvocationList(), apRemoved);
            }
        }

        protected override void SetItem(int index, AccountPrivilege item) {
            if (AccountPrivilegeChanged != null) {
                FrostbiteConnection.RaiseEvent(AccountPrivilegeChanged.GetInvocationList(), item);
            }

            base.SetItem(index, item);
        }
    }
}