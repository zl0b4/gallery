using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    public void Start()
    {
        
    }
    void Update()
    {
        //Output the network reachability to the console window
        //Check if the device cannot reach the internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            Params.IsInternetAvailable = false;
        }
        //Check if the device can reach the internet via a carrier data network
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Params.IsInternetAvailable = true;
        }
        //Check if the device can reach the internet via a LAN
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Params.IsInternetAvailable = true;
        }
    }
}