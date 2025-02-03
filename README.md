# core-simulated
Ceci est la version simulée de notre projet. Cela nous permet de nous affranchir des casques

- [Installation](#installation)
- [Utilisation](#utilisation)
- [Ressources](#ressources)
- [Contribution](#contribution)

## Installation

### Prérequis

Veuillez vous reporter au [README.md](https://github.com/realite-virtuelle-ksne/core/blob/main/README.md) du dépôt principal pour l’installation et la configuration de ces éléments.

### Cloner le dépôt

1. Cloner le dépôt
```bash
git clone https://github.com/realite-virtuelle-ksne/core-simulated.git
```
2. Ouvrir Unity Hub > Cliquer sur Add > **Sélectionner le dépôt**
3. Le projet s'ouvre dans Unity.


## Utilisation

Suivre la démarche suivante vous permettra d'obtenir le résultat suivant :

<div align="center">
  <img src="https://github.com/user-attachments/assets/a13539ab-3295-4171-8254-f69d2b9caf8f" alt="1_rendu_depuis_le_pc">
  <p><em>Figure 1 : Simulation du casque VR depuis un PC</em></p>
</div>

### Dépendances du dépôt

Vous n'avez rien à faire dans cette partie, mais comprendre les dépendances peut être utile :

- **XR Interaction Toolkit** (version 2.3.2, compatible avec Unity 2020.3) : permet les interactions en réalité virtuelle (RV). Pour plus d'informations sur les compatibilités de version, consultez la [partie ressources](#ressources)

- **XR Plugin Management** (incluant le `plugin Unity Mock HMD`) : Il permet d’afficher le contenu en réalité virtuelle, dans un casque ou non. Cela facilite le développement et le débogage directement depuis l'éditeur Unity.

`NB`: c'est le le `plugin Unity Mock HMD` qui permet de simuler l'affichage du casque VR sur votre pc, sans casque physique connecté.

### Une fois le projet ouvert (à faire une seule fois)

1. Import des samples

  - Aller dans `Window > Package Manager`
  - Assurez-vous d'avoir importé les samples `Starter Assets` et `XR Device Simulator` depuis XR Interaction Toolkit. Si c'est bien importé, vous verrez `Reimport` comme ci-dessous. Sinon, importez-les.

<div align="center">
  <img src="https://github.com/user-attachments/assets/c42368bd-918c-44a6-a816-340c73107966" alt="2_import_des_samples">
  <p><em>Figure 2 : Import des samples du package XR Interaction Toolkit</em></p>
</div>

2. Utilisation des Starter Assets

  - Aller dans `Assets > Samples > XR Interaction Toolkit > 2.3.2 > Starter Assets`
  - Ouvrir la scène `DemoScene`

<div align="center">
  <img src="https://github.com/user-attachments/assets/e6fd2f1f-3fe0-4c4a-9397-25d2bffeb1b6" alt="3_utilisation_stater_assets">
  <p><em>Figure 3 : Localisation et ouverture de la scène « DemoScene »</em></p>
</div>

3. Ajout du XR Device Simulator à la DemoScene

- Recherchez et trouvez le XR Device Simulator
- Glissez-déposez le XR Device Simulator dans la DemoScene
- Sauvegardez et appuyez sur `Start` !

<div align="center">
  <img src="https://github.com/user-attachments/assets/aae8e7a6-fb71-4b83-a27b-8aa217f571cb" alt="4_localiser_xr_device_simulator" style="display:inline-block; margin-right: 20px;">
  <img src="https://github.com/user-attachments/assets/f72b2a2a-6a94-4fe8-a32f-7355dfcd54da" alt="5_insertion_dans_demo_scene" style="display:inline-block;">
</div>
<div align="center">
  <p><em>Figure 4 : Localiser et ajouter le XR Device Simulator à la « DemoScene »</em></p>
</div>

4. Visualisation des associations (événements clavier-souris) et actions dans le simulateur

- Dans la hiérarchie, cliquez sur XR Device Simulator
- Dans le script associé à XR Device Simulator, allez dans Global Actions
- Dans le sous-champ `Device Simulator Actions Asset`, cliquez sur XR Device Simulator Controls

`NB`: Si vous souhaitez personnaliser ces associations, faites-le, mais uniquement dans votre branche !

<div align="center">
  <img src="https://github.com/user-attachments/assets/b6607508-e3aa-49de-ac00-9d49a69db7c4" alt="6_voir_les_associations_event_actions">
  <p><em>Figure 5 : Voir les associations événements - actions dans la scène virtuelle</em></p>
</div>

## Ressources

- Site internet

    - [Compatibilité unity - XR Interaction toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.3/manual/ui-setup.html)
    - [Set up de l'interaction Toolkit pour le développement RV](https://medium.com/@Brian_David/setup-xr-interaction-toolkit-for-vr-development-cd14af452943)

- Vidéos youtube

    - [S'habituer aux contrôles des manettes simulées](https://www.youtube.com/watch?v=iE5daijT-sg&t=319s)
    - [Utilisation du plugin Mock HMD de XR Plugin Management pour le device simulator](https://www.youtube.com/watch?v=CB6ViVbFqY0&t=722s)

## Contribution

Veuillez vous référer au [CONTRIBUTING.MD](https://github.com/realite-virtuelle-ksne/core/blob/main/CONTRIBUTING.md) pour contribuer au développement selon les règles fixées par l'équipe.
