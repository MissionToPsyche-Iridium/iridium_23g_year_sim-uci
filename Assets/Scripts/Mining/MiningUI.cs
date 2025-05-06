using UnityEngine;
using UnityEngine.UI;


public class MiningUI : MonoBehaviour
{
    [SerializeField] private Miner miner = null;
    private Mineable mine = null;

    [SerializeField] private Image resourceImage = null;
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private Image fillbarImage = null;

    public void initializeUI() {
      resourceImage.gameObject.SetActive(false);
      backgroundImage.gameObject.SetActive(false);
      miner.onEnterMine += SetMine;
      miner.onExitMine += ExitMine;
    }

    private void Update() {
      if (!mine) {
        return;
      }

      UpdateUI(mine.MiningProgress);
    }

    private void SetMine(Mineable m) {
       mine = m;
    }

    private void UpdateUI(float mineProgress) {
      resourceImage.gameObject.SetActive(mineProgress > 0);
      backgroundImage.gameObject.SetActive(mineProgress > 0);

      fillbarImage.fillAmount = mineProgress;
    }

    private void ExitMine() {
      mine = null;
      resourceImage.gameObject.SetActive(false);
      backgroundImage.gameObject.SetActive(false);
    }
}
