using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems
{

    public static class LeakTracker
    {
        private class Entry
        {
            public WeakReference Target;
            public string TypeName;
            public int Id;
            public DateTime Created;
        }

        private static readonly List<Entry> _entries = new List<Entry>();
        private static int _idCounter = 0;

        public static void Register(object obj)
        {
            _entries.Add(new Entry
            {
                Target = new WeakReference(obj),
                TypeName = obj.GetType().Name,
                Id = ++_idCounter,
                Created = DateTime.Now
            });
        }

        public static void ReportAlive()
        {
            int alive = 0;

            foreach (var e in _entries)
            {
                if (e.Target.IsAlive)
                {
                    DebugUtil.Log("LeakTracker",
                        $"{e.TypeName} #{e.Id} is STILL ALIVE " +
                        $"(created {e.Created:HH:mm:ss})", "magenta"
                    );
                    alive++;
                }
            }

            DebugUtil.Log("LeakTracker",$" Total alive: {alive}", "magenta");
        }

        public static void ForceCheck()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ReportAlive();
        }
    }

}