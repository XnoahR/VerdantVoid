// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class InteractionManager : MonoBehaviour
// {
//     public enum InteractionType { Objective, Normal, Pickable }
    
//     public List<CharacterInteraction> characterInteractions = new List<CharacterInteraction>();

//     private void CheckCurrentInteraction()
//     {
//         foreach (var interaction in characterInteractions)
//         {
//             if (interaction.chapter == GameplayMaster.currentChapter && interaction.stage == GameplayMaster.currentStage)
//             {
//                 var interactionObject = interaction.interactionObject;
//                 AssignInteraction(interactionObject, interaction.interactionType);
//                 break;
//             }
//         }
//     }

//     private void AssignInteraction(GameObject interactionObject, InteractionType interactionType)
//     {
//         // Remove any existing interaction scripts
//         var existingScripts = interactionObject.GetComponents<Item>();
//         foreach (var script in existingScripts)
//         {
//             Destroy(script);
//         }

//         // Add the new interaction script based on the interaction type
//         switch (interactionType)
//         {
//             case InteractionType.Objective:
//                 interactionObject.AddComponent<ObjectiveInteraction>();
//                 break;
//             case InteractionType.Normal:
//                 interactionObject.AddComponent<TestInteract>();
//                 break;
//             case InteractionType.Pickable:
//                 interactionObject.AddComponent<PickableItem>();
//                 break;
//         }
//     }

//     private void Update()
//     {
//         CheckCurrentInteraction();
//     }
// }
