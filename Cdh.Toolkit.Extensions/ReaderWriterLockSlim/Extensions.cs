using System;
using System.Threading;

namespace Cdh.Toolkit.Extensions.ReaderWriterLockSlim
{
    using ReaderWriterLockSlim = System.Threading.ReaderWriterLockSlim;

    public static class Extensions
    {
        public static LockHandle Read(this ReaderWriterLockSlim rwlock)
        {
            rwlock.EnterReadLock();
            return new LockHandle(rwlock, LockHandle.LockType.Read);
        }

        public static LockHandle UpgradeableRead(this ReaderWriterLockSlim rwlock)
        {
            rwlock.EnterUpgradeableReadLock();
            return new LockHandle(rwlock, LockHandle.LockType.UpgradeableRead);
        }

        public static LockHandle Write(this ReaderWriterLockSlim rwlock)
        {
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
