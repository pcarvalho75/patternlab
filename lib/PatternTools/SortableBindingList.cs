using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatternTools
{

    public class SortableBindingList<T> : BindingList<T>
    {
        // reference to the list provided at the time of instantiation
        List<T> _originalList;
        ListSortDirection _sortDirection;
        PropertyDescriptor _sortProperty;

        // function that refereshes the contents
        // of the base classes collection of elements
        Action<SortableBindingList<T>, List<T>> populateBaseList = (a, b) => a.ResetItems(b);

        class PropertyCompare : IComparer<T>
        {
            PropertyDescriptor _property;           // The propery value
            ListSortDirection _direction;           // The direction of compare

            public PropertyCompare(PropertyDescriptor property, ListSortDirection direction)
            {
                _property = property;
                _direction = direction;
            }

            public int Compare(T comp1, T comp2)
            {
                var value1 = _property.GetValue(comp1) as IComparable;
                var value2 = _property.GetValue(comp2) as IComparable;

                if (value1 == value2)
                {
                    return 0;
                }

                if (_direction == ListSortDirection.Ascending)
                {
                    if (value1 != null)
                    {
                        return value1.CompareTo(value2);
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (value2 != null)
                    {
                        return value2.CompareTo(value1);
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }

        public SortableBindingList()
        {
            _originalList = new List<T>();
        }

        public SortableBindingList(IEnumerable<T> enumerable)
        {
            _originalList = new List<T>(enumerable);
            populateBaseList(this, _originalList);
        }

        public SortableBindingList(List<T> list)
        {
            _originalList = list;
            populateBaseList(this, _originalList);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            PropertyCompare comp = new PropertyCompare(prop, _sortDirection);
            List<T> sortedList = new List<T>(this);
            sortedList.Sort(comp);
            ResetItems(sortedList);

            ResetBindings();

            // toggle sort
            _sortDirection = (_sortDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }


        protected override void RemoveSortCore()
        {
            ResetItems(_originalList);
        }

        private void ResetItems(List<T> items)
        {
            base.ClearItems();

            for (int i = 0; i < items.Count; i++)
            {
                base.InsertItem(i, items[i]);
            }
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                // indeed we do
                return true;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return _sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return _sortProperty;
            }
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            _originalList = base.Items.ToList();
        }
    }

}
