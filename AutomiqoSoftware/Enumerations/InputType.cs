namespace AutomiqoSoftware.Enumerations;

public enum InputType { // Enumeration (dropdown) for all interactable HTML elements for CV Engine

    // Operatables
    Button,
    Link,
    Anchor,
    Menu,
    Tab,
    Dropdown,
    Toggle,
    Checkbox,
    Radio,
    Slider,
    ProgressBar,

    // Text Inputs
    SingleLineInput,     // TextField, Search, Email, etc.
    MultiLineInput,      // TextArea
    PasswordInput,
    DateInput,
    TimeInput,
    NumberInput,
    UrlInput,
    TelephoneInput,

    // Upload Inputs
    FileSelector,        // FileUpload
    ImageSelector,       // ImageInput

    // Visual Elements
    StaticImage,         // <img>
    EmbeddedVideo,       // <video>
    DrawableCanvas,      // <canvas>
    ScalableGraphic,     // <svg>
    DecorativeIcon,      // <i> or <svg icon>
    TextLabel,           // <label>
    BlockText,           // <p>, styled <div>, etc.
    InlineText,          // <span>

    // Visual Containers
    InputForm,           // <form>
    VisualSection,       // <section>, <article>, <main>
    HeaderRegion,        // <header>
    FooterRegion,        // <footer>
    Sidebar,             // <aside>
    NavigationMenu,      // <nav>

    // Dynamic Elements
    ModalWindow,         // <modal>
    DialogBox,           // <dialog>
    TooltipPopup,        // <tooltip>
    EmbeddedFrame,       // <iframe>
    EmbeddedObject,      // <embed>, <object>

    // Informational UI Elements
    SelectList,          // <select>
    OptionItem,          // <option>
    ExpandableDetails,   // <details>
    ExpandableSummary,   // <summary>
    ValueMeter,          // <meter>
    OutputDisplay,       // <output>
    CodeBlock,           // <code>
    PreformattedText,    // <pre>

    // ErrorClause
    Unknown
}
