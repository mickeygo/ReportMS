using System;
using System.Runtime.ConstrainedExecution;

namespace ReportMS.Framework
{
    /// <summary>
    /// 表示派生类是可释放的对象
    /// </summary>
    public abstract class DisposableObject : CriticalFinalizerObject, IDisposable
    {
        #region Finalization Constructs

        /// <summary>
        /// 终结对象
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }

        #endregion

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing">释放对象应该被显示的释放</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.ExplicitDispose();
        }

        #endregion
    }
}
