using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Menus {
    public class CreditsBehaviour : MonoBehaviour {
        public TextMeshProUGUI CreditTitlePrefab;
        public TextMeshProUGUI NameCreditPrefab;
        public Transform Content;
        
        public List<CreditsSection> Credits = new List<CreditsSection> {
            CreditsSection.GetNew().SetTItle("").SetCreditList(CreditList.GetNew().Add("", "", "", "", "", "")),
            CreditsSection.GetNew().SetTItle("Programming").SetCreditList(CreditList.GetNew().Add("Luke Fisher", "Tanner Moore", "Logan Moore", "Tristan 'Stand up if you can' Havelick", "Mike Wingfield")),
            CreditsSection.GetNew().SetTItle("Art").SetCreditList(CreditList.GetNew().Add("Erik Meredith")),
            CreditsSection.GetNew().SetTItle("Music Composition").SetCreditList(CreditList.GetNew().Add("Tanner Moore")),
            CreditsSection.GetNew().SetTItle("SFX").SetCreditList(CreditList.GetNew().Add("Tanner Moore")),
            CreditsSection.GetNew().SetTItle("Level Design").SetCreditList(CreditList.GetNew().Add("Erik Meredith")),
            CreditsSection.GetNew().SetTItle("").SetCreditList(CreditList.GetNew().Add("", "", "", "", "")),
        };

        void Start() {
            Credits.ForEach(cs => {
                var title = Instantiate(CreditTitlePrefab, Content);
                title.text = cs.title;
                cs.list.names.ForEach(n => {
                    var nameCredit = Instantiate(NameCreditPrefab, Content);
                    nameCredit.text = n;
                });
            });
        }

        public void ScrolledToBottom() {
            StartCoroutine(WaitThenGo());
        }

        private IEnumerator WaitThenGo() {
            yield return new WaitForSeconds(0.001f);
            FadeToBlack.Instance.FadeOut(3, () => {
                SceneManager.LoadScene("WorldMap");
            });
        }
    }

    public class CreditsSection {
        public string title;
        public CreditList list; 
        
        public CreditsSection SetTItle(string title) {
            this.title = title;
            return this;
        }

        public CreditsSection SetCreditList(CreditList list) {
            this.list = list;
            return this;
        }

        public static CreditsSection GetNew() {
            return new CreditsSection();
        }
    }

    public class CreditList {
        public List<string> names;

        public CreditList() {
            names = new List<string>();
        }

        public CreditList Add(params string[] names) {
            this.names.AddRange(names);
            return this;
        }

        public static CreditList GetNew() {
            return new CreditList();
        }
    }
}