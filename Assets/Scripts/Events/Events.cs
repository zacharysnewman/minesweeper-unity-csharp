using System.Collections;
using System.Collections.Generic;

namespace Events
{
    namespace UnityEvents
    {
        public class AwakeEvent : EventSync { }
        public class StartEvent : EventSync { }
        public class UpdateEvent : EventSync { }
        public class FixedUpdateEvent : EventSync { }
        public class LateUpdateEvent : EventSync { }
        public class DestroyEvent : EventSync { }
        public class ApplicationQuitEvent : EventSync { }
    }
    namespace InputEvents
    {
        public class GenerateMapEvent : EventSync<MapInformation> { }
        public class ActivateTileEvent : EventSync<Coords, bool> { }
    }
    namespace StateEvents
    {
        public class StateChangedEvent : EventSync<State> { }
    }
}