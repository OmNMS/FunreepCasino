using ViewModel;

namespace Components
{
    public interface IRewardTimer
    {
         ulong LastChestOpen();
         bool IsRewardReady(RewardFortune rewardFortune, float SecondsToWait);
         string CalculateTimer(float secondsToWait);
    }
}
