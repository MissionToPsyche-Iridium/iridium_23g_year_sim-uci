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

    public Button magButton;

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

    public ResearchPaperLock paperLock;
    public PopUpManager popUpManager;


    private void OnEnable()
    {
        
        coreButton.onClick.AddListener(() => HandleClick("Core", topicCoreImage, GetCoreText()));
        metalButton.onClick.AddListener(() => HandleClick("Metals", topicMetalImage, GetMetalText()));
        orbitButton.onClick.AddListener(() => HandleClick("Orbit & Rotation", topicOrbitImage, GetOrbitText()));
        psycheShapeButton.onClick.AddListener(() => HandleClick("Psyche Shape", topicPsycheShapeImage, GetShapeText()));
        tempButton.onClick.AddListener(() => HandleClick("Temperature and Weather", topicTempAndWeathImage, GetTempText()));
        historyButton.onClick.AddListener(() => HandleClick("Psyche History", topicPsycheHistoryImage, GetHistoryText()));
        timelineButton.onClick.AddListener(() => HandleClick("Psyche Mission Timeline", topicPsycheTimelineImage, GetTimelineText()));
        magnetometerButton.onClick.AddListener(() => HandleClick("Magnetometer", topicMagnetometerImage, GetMagnetometerText()));
        multispectralButton.onClick.AddListener(() => HandleClick("Multispectral Imager", topicMultispectralImage, GetMultispectralText()));
        gammaRayButton.onClick.AddListener(() => HandleClick("Gamma-Ray and Neutron Spectrometer", topicGammaRayImage, GetGammaRayText()));
    }
    private void HandleClick(string key, Sprite image, string content)
    {
        if (paperLock != null && !paperLock.IsUnlocked(key))
        {
            topicTitle.text = "???";
            topicViewContent.text = "This research paper is currently locked. Explore more to unlock it.";
            topicImage.sprite = topicDefaultImage;
            return;
        }

        topicTitle.text = key;
        topicViewContent.text = content;
        topicImage.sprite = image;
    }


    private string GetCoreText() => "Scientists think Psyche may consist of significant amounts of metal from the core of a planetesimal, one of the building blocks of our solar system. The asteroid is most likely a survivor of multiple violent hit-and-run collisions, common when the solar system was forming. The asteroid’s composition has been determined by radar observations and by the measurements of the asteroid’s thermal inertia (how quickly an object gains or re-radiates heat). The best analysis indicates that Psyche is likely made of a mixture of rock and metal, with metal comprising between 30-60% of its volume, however we won’t know until we arrive at Psyche.";
    private string GetMetalText() => "While Psyche’s composition is not entirely known, there’s speculation and evidence that shows that Psyche may contain a large fraction of a metal core and some silicate mantle retained on the outside, or the silicate fraction could be mixed into the metal fraction. It’s estimated that the largest concentration of “metal” elements being considered for Psyche consist of Iron (Fe) and Nickel (Ni). It’s estimated that it is composed of 30–55 vol.% Iron metal. Iron can be found in various places on Earth and used in everyday items such as cars, cutlery and pipes. Nickel can also be found in various everyday items such as jewelry, coins, and electronics. The total iron content (in metal and silicates) will be determined from element data acquired by the GammaRay and Neutron Spectrometer.";
    private string GetOrbitText() => "Psyche follows an orbit in the outer part of the main asteroid belt between Mars and Jupiter., at an average distance from the Sun of 3 astronomical units; Earth orbits at 1 AU.  An AU is about 93 million miles of distance (the distance from the Sun to the Earth). Psyche takes about five Earth years to complete one orbit of the Sun, but it takes just over four hours to rotate once on its axis (a Psyche “day”). Fun Fact: The Psyche asteroid does not rotate like Earth. Earth’s axis is a tilted, imaginary pole going right through the center of Earth from ‘top’ to ‘bottom.’ Earth spins around this pole, making one complete turn each day. But Psyche’s axis is turned on its side compared to Earth’s! So, if you imagine Earth rotating like a child’s toy top, you can imagine that Psyche instead rotates on its side like a rotisserie chicken!";
    private string GetShapeText() => "If Psyche were a perfect sphere, it would have a diameter of 140 miles (226 kilometers), or about the length of the State of Massachusetts (leaving out Cape Cod). It is estimated to have a surface area of about 64,000 square miles. Psyche is not spherical but rather closer to a potato-like shape.  By combining radar and optical observations, scientists generated a 3D model of Psyche that shows evidence of two craterlike depressions. It suggests that there is significant variation in the metal content and color of the asteroid over its surface. Asteroid Psyche is suggested to have at least one large impact crater near its south pole, similar to that found on (4) Vesta asteroid!";
    private string GetTempText() => "On Earth, a tilted axis causes the seasons. Throughout the year, different parts of Earth receive the Sun’s most direct rays. So, when the North Pole tilts toward the Sun, it’s summer in the Northern Hemisphere. And when the South Pole tilts toward the Sun, it’s winter in the Northern Hemisphere. Psyche also has seasons, but not like we are used to thinking of them here on Earth. Since Psyche rotates almost completely on its side, its surface experiences extended periods of direct sunlight at one pole (when that pole is experiencing its summer) and extended periods of complete darkness at the other pole (when that pole is experiencing its winter). Scientists have modeled that the warmest temperature at Psyche is still very cold by our standards: around -100 degrees Fahrenheit (-70 degrees Celsius). But the coldest? A lot colder! At one of the poles in the middle of its winter (when that pole is getting no sun at all), Psyche could be around -340F (-200C). The stress of this extreme weather affects Psyche Landscape, it can lead to cracking and breaking down of large boulders on the asteroid.";
    private string GetHistoryText() => "The asteroid was discovered in 1852 by Italian astronomer Annibale de Gasparis; because it was the 16th asteroid to be discovered, it is sometimes referred to as 16 Psyche. The asteroid was named for the goddess of the soul in ancient Greek mythology, often depicted as a butterfly-winged female figure. Asteroid 16 Psyche is the largest asteroid crossing Mar’s orbit and a target of the NASA Discovery mission Psyche. This will be NASA’s first space mission to a world rich in metal, rather than rock or ice. Psyche is most likely a survivor of multiple violent hit-and-run collisions, common when the solar system was forming. The asteroid Psyche may be able to show us how Earth’s core and the cores of the other terrestrial planets came to be.";
    private string GetTimelineText() => "The Psyche Mission consists of six phases, labeled A-F.  These phases span from initial studies and selection of the mission by NASA for its Discovery Program, through building and testing of instruments and the spacecraft, to launch, cruise, and arrival at the asteroid and finally to orbit and the closeout of the mission. Psyche launched on October 13, 2023 aboard a SpaceX Falcon Heavy rocket at NASA’s Kennedy Space Center in Florida. The Psyche satellite expects to arrive at the asteroid by August 2029, The spacecraft will travel 2.2 billion miles on its journey from Earth to the asteroid. The Psyche mission will have four stages of orbits. Orbits A through D focus on different phases of Psyche research. With each orbit it becomes successively closer to the asteroid. All four orbit stages are expected to take place during the 26 months of the primary mission. Orbit A focuses on characterization of Psyche, Orbits B1 and B2, which are split, focus on topography, Orbit C focuses on the gravity science and finally, Orbit D focuses on elemental data and mapping.";
    private string GetMagnetometerText() => "The Psyche satellite is equipped with a magnetometer. This tool detects magnetic fields around Psyche to detect remanent magnetism. A residual magnetic field would be strong evidence the asteroid formed from the core of a planetary body. When an object is magnetized it still continues to retain some magnetization even after an external magnetic field is gone. Earth has a magnetic field, and it helps protect us. This field helps serve as a shield to protect us from solar radiation and particles. The surface gravity on Psyche is much less than it is on Earth – even less than it is on Earth’s Moon. On Psyche, lifting a car would feel like lifting a large dog. Once the Psyche satellite reaches the Psyche asteroid it will orbit Psyche to study it. The magnetometer is composed of two identical high-sensitivity magnetic field sensors located at the middle and outer end of a 6-foot pole.";
    private string GetMultispectralText() => "The multispectral imager is similar to a camera, however it’ is able to capture much more information. It captures different data across the electromagnetic spectrum that are visible to the human eye such as color but also UV and Infrared wave-lengths. This tool is equipped on Psyche to provide high-resolution images using filters in order to discriminate between Psyche’s metallic and silicate constituents. The instrument consists of a pair of identical cameras designed to obtain geologic, compositional, and topographic data. This exploration takes place during Orbit B (split into Orbits B1 and B2), a stage that focuses on studying Psyche’s topography and also composition.";
    private string GetGammaRayText() => "The Gamma-Ray and Neutron Spectrometer is another tool the Psyche satellite is equipped with. This tool will detect, measure, and map Psyche’s chemical composition that makes up the asteroid. By understanding its composition, it will help scientists figure out how it formed. It looks for hydrogen, metals and other elements from orbit without having to dig into the Psyche asteroid.";
}
   
           