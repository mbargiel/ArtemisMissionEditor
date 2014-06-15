﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace ArtemisMissionEditor
{
    public sealed class DictionaryPropertyGridAdapter : ICustomTypeDescriptor
    {
        IDictionary _dictionary;

        #region Boring Stuff

        public DictionaryPropertyGridAdapter(IDictionary d) { _dictionary = d; }
        public string GetComponentName() { return TypeDescriptor.GetComponentName(this, true); }
        public EventDescriptor GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }
        public string GetClassName() { return TypeDescriptor.GetClassName(this, true); }
        public EventDescriptorCollection GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }
        EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents() { return TypeDescriptor.GetEvents(this, true); }
        public TypeConverter GetConverter() { return TypeDescriptor.GetConverter(this, true); }
        public object GetPropertyOwner(PropertyDescriptor pd) { return _dictionary; }
        public AttributeCollection GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }
        public object GetEditor(Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }
        public PropertyDescriptor GetDefaultProperty() { return null; }

        #endregion

        PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        //We iterate over the IDictionary, creating a property descriptor for each entry:
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            ArrayList properties = new ArrayList();
            foreach (DictionaryEntry e in _dictionary)
            {
                properties.Add(new DictionaryPropertyDescriptor(_dictionary, e.Key));
            }

            PropertyDescriptor[] props =
                (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

            return new PropertyDescriptorCollection(props);
        }
    }

}
