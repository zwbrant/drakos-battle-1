using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityStringEvent : UnityEvent<string> { }

[Serializable]
public class UnityStringArrayEvent : UnityEvent<string[]> { }

[Serializable]
public class UnityGameObjEvent : UnityEvent<GameObject> { }

[Serializable]
public class UnityGameStateEvent : UnityEvent<ClientGameState> { }

[Serializable]
public class UnityVector3Event : UnityEvent<Vector3> { }

[Serializable]
public class UnityIntEvent : UnityEvent<int?> { }