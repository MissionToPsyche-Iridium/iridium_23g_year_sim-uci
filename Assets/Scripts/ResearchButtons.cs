using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchButtons : MonoBehaviour
{
    public Button coreButton;
    public Button metalButton;
    public Button orbitButton;
    public Button psycheShapeButton;
    public Button tempButton;
    public Button historyButton;
    public Button timelineButton;
    public Button magnetometerButton;
    public Button multispectralButton;
    public Button gammaRayButton;

    [SerializeField] private TMP_Text topicTitle;
    [SerializeField] private TMP_Text topicViewContent;

    public Image topicImage;
    public Sprite topicDefaultImage;
    public Sprite topicCoreImage;
    public Sprite topicMetalImage;
    public Sprite topicOrbitImage;

    public Sprite topicPsycheShapeImage;
    public Sprite topicTempAndWeathImage;
    public Sprite topicPsycheHistoryImage;
    public Sprite topicPsycheTimelineImage;
    public Sprite topicMagnetometerImage;
    public Sprite topicMultispectralImage;
    public Sprite topicGammaRayImage;

    private void OnEnable()
    {
        coreButton.onClick.AddListener(() => OnButtonClicked(coreButton));
        metalButton.onClick.AddListener(() => OnButtonClicked(metalButton));
        orbitButton.onClick.AddListener(() => OnButtonClicked(orbitButton));
        psycheShapeButton.onClick.AddListener(() => OnButtonClicked(psycheShapeButton));
        tempButton.onClick.AddListener(() => OnButtonClicked(tempButton));
        historyButton.onClick.AddListener(() => OnButtonClicked(historyButton));
        timelineButton.onClick.AddListener(() => OnButtonClicked(timelineButton));
        magnetometerButton.onClick.AddListener(() => OnButtonClicked(magnetometerButton));
        multispectralButton.onClick.AddListener(() => OnButtonClicked(multispectralButton));
        gammaRayButton.onClick.AddListener(() => OnButtonClicked(gammaRayButton));
    }

    private void OnButtonClicked(Button clickedButton)
    {
        if (clickedButton == coreButton)
        {
            topicTitle.text = "Core";
            topicViewContent.text = "Scientists think Psyche may consist of significant amounts of metal from the core of a planetesimal, one of the building blocks of our solar system. The asteroid is most likely a survivor of multiple violent hit-and-run collisions, common when the solar system was forming. The asteroid’s composition has been determined by radar observations and by the measurements of the asteroid’s thermal inertia (how quickly an object gains or re-radiates heat). The best analysis indicates that Psyche is likely made of a mixture of rock and metal, with metal comprising between 30-60% of its volume, however we won’t know until we arrive at Psyche. ";
            topicImage.sprite = topicCoreImage;
        }
        else if (clickedButton == metalButton)
        {
            topicTitle.text = "Metals";
            topicViewContent.text = "While Psyche’s composition is not entirely known, there’s speculation and evidence that shows that Psyche may contain a large fraction of a metal core and some silicate mantle retained on the outside, or the silicate fraction could be mixed into the metal fraction. It’s estimated that the Largest concentration of “metal” elements being considered for Psyche consist of Iron (Fe) and Nickel (Ni).It’s estimated that it is composed of 30–55 vol.% Iron metal. Iron can be found in various places on earth and used in everyday items such as cars, cutlery and pipers. Nickel can also be found in various everyday items such as jewelry, coins, and electronics.The total iron content (in metal and silicates) will be determined from element data acquired by the Gamma Ray and Neutron Spectrometer";
            topicImage.sprite = topicMetalImage;
        }
        else if (clickedButton == orbitButton){
            topicTitle.text = "Orbit & Rotation";
            topicViewContent.text = "Psyche follows an orbit in the outer part of the main asteroid belt between Mars and Jupiter., at an average distance from the Sun of 3 astronomical units; Earth orbits at 1 AU.  An Au is about 235 to 309 million miles of distance. Psyche takes about five Earth years to complete one orbit of the Sun, but it takes just over four hours to rotate once on its axis (a Psyche “day”). Fun Fact: The psyche asteroid does not rotate like earth on its axis instead Psyche rotates horizontally like a rotisserie chicken!";
            topicImage.sprite = topicOrbitImage;
        }
        else if (clickedButton == psycheShapeButton){
             topicTitle.text = "Psyche Shape";
            topicViewContent.text = "If Psyche were a perfect sphere, it would have a diameter of 140 miles (226 kilometers), or about the length of the State of Massachusetts (leaving out Cape Cod). It is estimated to have a surface area of about 64,000 square miles Psyche is not spherical but rather closer to a potato like shape.  By combining radar and optical observations, scientists generated a 3D model of Psyche that shows evidence of two craterlike depressions. It suggests that there is significant variation in the metal content and color of the asteroid over its surface. ";
            topicImage.sprite = topicPsycheShapeImage;
        }
        else if (clickedButton == tempButton){
             topicTitle.text = "Temperature and Weather";
            topicViewContent.text = "Psyche’s surface temperatures have a large impact on the material properties of the surface, Psyche has an extreme axis tilt of about 95°, which is a high obliquity, in comparison earth has low-obliquity. Obliquity affects the planet’s climate. In a low-obliquity world the equatorial areas of plants receive periods of direct overhead insolation (solar radiation), meanwhile the poles near the axis have a much more muted exposure meaning longer periods of darkness. On psyche a high- obliquity world, most of the surface experiences both periods of direct overhead insolation and extended periods of darkness. Its orbital configuration and thermal stress affected from its high-obliquity can lead to cracking and breaking down of large boulders on the asteroid, affecting its landscape. ";
            topicImage.sprite = topicTempAndWeathImage;
        }
        else if (clickedButton == historyButton){
            topicTitle.text = "Psyche History";
            topicViewContent.text = "The asteroid was discovered in 1852 by Italian astronomer Annibale de Gasparis because it was the 16th asteroid to be discovered, it is sometimes referred to as 16 Psyche. The asteroid was named for the goddess of the soul in ancient Greek mythology, often depicted as a butterfly-winged female figure. Asteroid 16 Psyche is the largest mars crossing,  (1984) M-class asteroid and a target of the NASA Discovery mission Psyche. This will be NASA’s first space mission to a world rich in metal, rather than rock or ice. Psyche is most likely a survivor of multiple violent hit-and-run collisions, common when the solar system was forming. The asteroid Psyche may be able to show us how Earth’s core and the cores of the other terrestrial planets came to be.";
            topicImage.sprite = topicPsycheHistoryImage;
        }
        else if (clickedButton == timelineButton){
            topicTitle.text = "Psyche Mission Timeline";
            topicViewContent.text = "The Psyche Mission consists of six phases, labeled A-F.  These phases span from initial studies and selection of the mission by NASA for its Discovery Program, through building and testing of instruments and the spacecraft, to launch, cruise, and arrival at the asteroid and finally to orbit and the closeout of the mission. Psyche launched on October 13, 2023 aboard a SpaceX Falcon Heavy rocket at NASA’s Kennedy Space Center in Florida. The psyche satellite expects to arrive at the asteroid by august 2029, The spacecraft will travel 2.2 billion miles from earth. The psyche mission will have four stages of orbits. Orbit A through D focus on different phases of psyche research. With each orbit it becomes successively closer to the psyche. All four orbit stages are expected to take place during the last 21 months of the mission. Orbit A focusing on characterization of psyche, Orbit B focuses on topography, Orbit C focusing on the Gravity science revolving around psyche and finally, Orbit D analyzing elemental data and mapping.";
            topicImage.sprite = topicPsycheTimelineImage;
            }
        else if (clickedButton == magnetometerButton){
            topicTitle.text = "Magnetometer";
            topicViewContent.text = "The Psyche satellite is equipped with a magnetometer. This tool detects magnetic fields around Psyche to detect remnant magnetism. A residual magnetic field would be strong evidence the asteroid formed from the core of a planetary body. When an object is magnetized it still continues to retain some magnetization even after an external magnetic field is gone. Earth has a magnetic field, and it helps protect us. This field helps serve as a shield to protect us from solar radiation and particles. The surface gravity on Psyche is much less than it is on Earth – even less than it is on Earth’s Moon. On Psyche, lifting a car would feel like lifting a large dog. Once the psyche satellite reaches the psyche asteroid it will orbit Psyche to study it. It is composed of two identical high-sensitivity magnetic field sensors located at the middle and outer end of a 6-foot pole.";
            topicImage.sprite = topicMagnetometerImage;
        }
        else if (clickedButton == multispectralButton){
           topicTitle.text = "Multispectral Imager";
            topicViewContent.text = "The multispectral imager is similar to a camera, however it’s able to capture much more information. It captures different data across the electromagnetic spectrum that are visible to the human eye such as color but also UV and Infrared wave-lengths. This tool is equipped on psyche to provide high-resolution images using filters in order to discriminate between Psyche’s metallic and silicate constituents. The instrument consists of a pair of identical cameras designed to obtain geologic, compositional, and topographic data. This exploration takes place during Orbit B, a stage that focuses on studying Psyche’s topography and also composition." ;
            topicImage.sprite = topicMultispectralImage;
        }
        else if (clickedButton == gammaRayButton){
            topicTitle.text = "Gamma-Ray and Neutron Spectrometer ";
            topicViewContent.text = "The Gamma-Ray and Neutron Spectrometer is another tool Psyche satellite is equipped with. This tool will detect, measure, and map Psyche’s chemical composition that makes up the asteroid. By understanding its composition, it will help scientists figure out how it’s formed. It looks for hydrogen, metals and other elements without having to dig into the Psyche asteroid.";
            topicImage.sprite = topicGammaRayImage;
        }
    }
}
