using Normal.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eOwnedType { None, Local, Remote}
public class DisableObjectWhenOwned : MonoBehaviour
{

    public eOwnedType type = eOwnedType.Local;
    // Start is called before the first frame update
    void Start()
    {
        RealtimeView view = transform.root.GetComponent<RealtimeView>();
        if (view != null)
        {
            switch (type)
            {
                case eOwnedType.None:
                    if(view.ownerIDSelf == -1)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case eOwnedType.Local:
                    if (view.isOwnedLocallySelf)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case eOwnedType.Remote:
                    if (view.isOwnedRemotelySelf)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }


}
