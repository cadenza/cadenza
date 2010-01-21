using System;
using System.Threading;

namespace Cdh.Toolkit.Extensions.ReaderWriterLockSlim
{
    using ReaderWriterLockSlim = System.Threading.ReaderWriterLockSlim;

    public static class Extensions
    {
        public static LockHandle Read(this ReaderWriterLockSlim rwlock)
        {
            if (rwlock.IsReadLockHeld || rwlock.IsUpgradeableReadLockHeld || rwlock.IsWriteLockHeld)
                return new LockHandle();

            rwlock.EnterReadLock();
            return new LockHandle(rwlock, LockHandle.LockType.Read);
        }

        public static LockHandle UpgradeableRead(this ReaderWriterLockSlim rwlock)
        {
            if (rwlock.IsUpgradeableReadLockHeld || rwlock.IsWriteLockHeld)
                return new LockHandle();

            rwlock.EnterUpgradeableReadLock();
            return new LockHandle(rwlock, LockHandle.LockType.UpgradeableRead);
        }

        public static LockHandle Write(this ReaderWriterLockSlim rwlock)
        {
            if (rwlock.IsWriteLockHeld)
                return new LockHandle();

            rwlock.EnterWriteLock();
            return new LockHandle(rwlock, LockHandle.LockType.Write);
        }

        public struct LockHandle : IDisposable
        {
            internal enum LockType
            {
                Read,
                UpgradeableRead,
                Write
            }

            private ReaderWriterLockSlim rwlock;
            private LockType type;

            internal LockHandle(ReaderWriterLockSlim rwlock, LockType type)
            {
                this.rwlock = rwlock;
                this.type = type;
            }

            public void Dispose()
            {
                ReaderWriterLockSlim copy = Interlocked.Exchange(ref rwlock, null);

                if (copy == null)
                    return;

                switch (type)
                {
                    case LockType.Read:
                        copy.ExitReadLock();
                        break;

                    case LockType.UpgradeableRead:
                        copy.ExitUpgradeableReadLock();
                        break;

                    case LockType.Write:
                        copy.ExitWriteLock();
                        break;
                }
            }
        }
    }
}
