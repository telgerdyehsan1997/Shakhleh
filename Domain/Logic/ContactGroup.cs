namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Olive.Entities;
    using Olive.Entities.Data;

    /// <summary> 
    /// Provides the business logic for ContactGroup class.
    /// </summary>
    partial class ContactGroup
    {
        /// <summary>
        /// Update contacts for this contact group.
        /// </summary>
        public async Task UpdateContacts(List<Person> newContacts)
        {
            var existingContacts = (await Contacts).ToList();
            var contactsUnSelected = existingContacts.Except(newContacts).ToList();

            var itemsToDelete = (await ContactsLinks.GetList()).Where(l => contactsUnSelected.Contains(l.Person)).ToList();

            var itemsToSave = newContacts.Except(existingContacts).Select(contact => new ContactGroupContactsLink
            {
                Person = contact,
                Contactgroup = this
            }).ToList();


            using (var scope = Database.CreateTransactionScope())
            {
                await Database.Delete(itemsToDelete);

                await Database.Save(itemsToSave);

                scope.Complete();
            }
        }
    }
}