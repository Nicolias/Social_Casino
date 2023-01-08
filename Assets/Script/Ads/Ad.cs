static class Ad
{
    public static void ShowRewardAd()
    {
        if (Advertisements.Instance.IsRewardVideoAvailable())
        {            
            Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
        }
    }

    private static void CompleteMethod(bool completed, string advertiser)
    {
        if (Advertisements.Instance.debug)
        {
            if (completed == true)
            {
                //give the reward
            }
            else
            {
                //no reward
            }
        }
    }
}