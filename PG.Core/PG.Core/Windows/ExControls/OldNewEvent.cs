﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PG.Core.Windows.ExControls
{
    public class OldNewEventArgs<T> : EventArgs
    {
        public OldNewEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue
        {
            get { return this.m_oldValue; }
            protected set { this.m_oldValue = value; }
        }
        public T NewValue
        {
            get { return this.m_newValue; }
            protected set { this.m_newValue = value; }
        }

        T m_oldValue = default(T);
        T m_newValue = default(T);
    }

    public delegate void OldNewEventHandler<T>(object sender, OldNewEventArgs<T> e);
}
