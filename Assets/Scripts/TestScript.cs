using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public CharacterSheet sheet { get; set; }

    void Start()
    {
        sheet = new CharacterSheet();
        //sheet.Name = "Wendel";
        //Advantage ad = new Advantage();
        //ad.Name = "+1 ST";
        //ad.Description = "+1 STrength";
        //ad.AddEffect(new STBuff());

        //Skill sk = new Skill();
        //sk.Name = "HIt";
        //sk.SkillLevel = 12;

        //Item it = new Item();
        //it.AddSkillName("HIt");
        //it.IsEquipped = false;
        //it.Damage = "A lot";
        //it.Weight = 3.0f;
        //it.Name = "Sword";
        //it.Value = 89;
        //it.TL = 6.0f;
        //it.Quantity = 7.0f;

        //sheet.AddAdvantage(ad);
        //sheet.AddItem(it);
        //sheet.AddSkill(sk);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("////////////////////////New Sheet//////////////////////////////////////");
            Debug.Log(sheet.Name);
            Debug.Log(sheet.ST);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    string path = "C:/Users/Carson Sena/Characters/test.json";
        //    string jsonString = JsonUtility.ToJson(sheet.GetSaveData());
        //    System.IO.File.WriteAllText(path, jsonString);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            string path = "C:/Users/Carson Sena/Characters/test.json";
            string jsonString = System.IO.File.ReadAllText(path);
            CharacterSaveData data = JsonUtility.FromJson<CharacterSaveData>(jsonString);
            sheet = new CharacterSheet(data);
        }
    }
}
