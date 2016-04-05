using System;
using System.Data;

namespace PersistentLayer.Impl
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PersistentLayer.Impl.TransactionInfo" />
    /// <seealso cref="PersistentLayer.ITransactionWorker" />
    public class TransactionWorkerImpl
        : TransactionInfo, ITransactionWorker
    {
        private TransactionStatus status = TransactionStatus.WaitingFor;
        private readonly Action<TransactionDescriptor> onBegin;
        private readonly Action<TransactionDescriptor> onCommit;
        private readonly Action<TransactionDescriptor> onRollback;
        private readonly TransactionDescriptor descriptor;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWorkerImpl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="onBegin">The on begin.</param>
        /// <param name="onCommit">The on commit.</param>
        /// <param name="onRollback">The on rollback.</param>
        /// <param name="begin">if set to <c>true</c> [begin].</param>
        public TransactionWorkerImpl(string name, Action<TransactionDescriptor> onBegin, Action<TransactionDescriptor> onCommit, Action<TransactionDescriptor> onRollback, bool begin = true)
            : this(new TransactionDescriptor{ Name = name}, onBegin, onCommit, onRollback, begin)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionWorkerImpl"/> class.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="onBegin">The on begin.</param>
        /// <param name="onCommit">The on commit.</param>
        /// <param name="onRollback">The on rollback.</param>
        /// <param name="begin">if set to <c>true</c> [begin].</param>
        public TransactionWorkerImpl(TransactionDescriptor descriptor, Action<TransactionDescriptor> onBegin, Action<TransactionDescriptor> onCommit, Action<TransactionDescriptor> onRollback, bool begin = true)
            : base(descriptor.Name, -1)
        {
            this.descriptor = descriptor;
            this.onBegin = onBegin;
            this.onCommit = onCommit;
            this.onRollback = onRollback;
            this.disposed = false;
            this.OnInit(begin);
        }
        
        private void OnInit(bool begin)
        {
            if (begin)
            {
                this.Begin();
            }
        }
        
        public TransactionStatus Status { get { return this.status; } }
        
        public void Begin()
        {
            this.EnsureDisposing("Begin");
            if (this.Status == TransactionStatus.WaitingFor)
            {
                this.onBegin(this.descriptor);
                this.status = TransactionStatus.InProgress;
            }
        }

        public virtual void Begin(IsolationLevel? isolationLevel)
        {
            this.descriptor.Isolation = isolationLevel;
            this.Begin();
        }

        public virtual void Commit()
        {
            this.EnsureDisposing("Commit");

            if (this.Status == TransactionStatus.InProgress)
            {
                this.status = TransactionStatus.Committed;
                this.onCommit(this.descriptor);
            }
        }

        public virtual void Rollback()
        {
            this.EnsureDisposing("Rollback");

            if (this.Status == TransactionStatus.InProgress)
            {
                this.status = TransactionStatus.RolledBack;
                this.onRollback(this.descriptor);
            }
        }

        public void Dispose()
        {
            this.EnsureDisposing("Dispose");

            try
            {
                if (this.Status == TransactionStatus.InProgress)
                {
                    this.status = TransactionStatus.RolledBack;
                    this.onRollback(this.descriptor);
                }

                this.disposed = true;
            }
            catch (Exception)
            {
                this.disposed = true;
                throw;
            }
        }

        private void EnsureDisposing(string operation)
        {
            if (this.disposed)
                throw new ObjectDisposedException(string.Format("The current transaction cannot be used anymore because It was disposed, operation: {0}", operation));
        }
    }
}
