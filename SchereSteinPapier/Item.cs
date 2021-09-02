using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace SchereSteinPapier
{
    /// <summary>
    /// This item class is to be used for the game selection objects e.g. paper, stone or scissors.
    /// </summary>
    public class Item : ObservableObject
    {
        // Private Fields
        private string name;
        private string imageFilePath;
        private List<Item> strongerThan;

        // Properties
        public string Name { get => name; }
        public string ImageFilePath { get => imageFilePath; }
        internal List<Item> StrongerThan { get => strongerThan; }

        internal Item(string name, string imageFilePath)
        {
            this.name = name;
            this.imageFilePath = imageFilePath;
            this.strongerThan = new List<Item>();
        }

        /// <summary>
        /// To define a selection-object as weaker than this instance you have to add it to the "StrongerThan"-List with this method.
        /// The item is only added if there is no contradiction. 
        /// </summary>
        /// <param name="weakerItem">Returns true if the item was added. Returns false if there is a contradiction.</param>
        internal bool AddWeakerItem(Item weakerItem)
        {
            // Adds the weaker item only if this instance is not "weaker" already.
            if (!weakerItem.StrongerThan.Contains(this))
            {
                strongerThan.Add(weakerItem);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
