# Comentsys Community Controls

Comentsys Community Controls is a set of Universal Windows Platform Controls available for developing Windows 10 applications

## Dice

Dice control that can display values that resembles a single Dice or Die face

### Syntax

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Dice Height="100" Width="100" Background="Red" Foreground="White" CornerRadius="10" Value="3"/>
```

### Output

![Dice Control](Resources/Dice.png)

### Properties

| Property | Type | Description |
| -- | -- | -- |
| Foreground | Brush | Gets or sets the foreground colour of a single dice |
| Background | Brush | Gets or sets the background colour of a single dice |
| Value | int | Gets or sets the value to display on a single dice between 0 and 6 |
| CorderRadius | CornerRadius | Gets or sets the Corner Radius of the control |

### Examples

**XAML**

```xaml
<comentsys:Dice Height="100" Width="100" Background="White" Foreground="Black" CornerRadius="5" Value="5"/>
```

**C#**

```csharp
Dice dice = new Dice()
{
    Height = 100,
    Width = 100,
    Background = new SolidColorBrush(Colors.White),
    Foreground = new SolidColorBrush(Colors.Black),
    CornerRadius = new CornerRadius(5),
    Value = 5
};
```

## Clock

Clock control that resembles an analogue clock

### Syntax

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Clock Height="300" Width="300" RimBackground="WhiteSmoke" RimForeground="Black" SecondHandForeground="Blue"/>
```

### Output

![Clock Control](Resources/Clock.png)

### Properties

| Property | Type | Description |
| -- | -- | -- |
| Fill | Brush | Gets or sets the fill colour of the clock |
| RimBackground | Brush | Gets or sets the background colour of the rim of the clock |
| RimForeground | Brush | Gets or sets the foreground colour of the rim of the clock |
| SecondHandForeground | Brush | Gets or sets the foreground colour of the second hand of the clock |
| MinuteHandForeground | Brush | Gets or sets the foreground colour of the minute hand of the clock |
| HourHandForeground | Brush | Gets or sets the foreground colour of the hour hand of the clock |
| ShowSecondHand | bool | Gets or sets value to show or hide the second hand of the clock |
| ShowMinuteHand | bool | Gets or sets value to show or hide the minute hand of the clock |
| ShowHourHand | bool | Gets or sets value to show or hide the hour hand of the clock |
| IsRealTime | bool | Gets or sets if the clock should be real time |
| Value | DateTime | Gets or sets the value to display on the clock if not real time |

### Examples

**XAML**

```xaml
<comentsys:Clock Height="300" Width="300" RimBackground="Black" RimForeground="White"  HourHandForeground="Red" MinuteHandForeground="Green" SecondHandForeground="Blue"/>
```

**C#**

```csharp
Clock clock = new Clock()
{
    Height = 300,
    Width = 300,
    RimBackground = new SolidColorBrush(Colors.Black),
    RimForeground = new SolidColorBrush(Colors.White),
    HourHandForeground = new SolidColorBrush(Colors.Red),
    MinuteHandForeground = new SolidColorBrush(Colors.Green),
    SecondHandForeground = new SolidColorBrush(Colors.Blue)
};
```

## Dial

Dial control that can be used to set a value between a minimum and maximum value

### Syntax

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Dial Height="200" Width="200" DialForeground="WhiteSmoke" DialBackground="Teal"/>
```

### Output

![Dial Control](Resources/Dial.png)

### Properties

| Property | Type | Description |
| -- | -- | -- |
| DialBackground | Brush | Gets or sets the background colour of the dial |
| DialForeground | Brush | Gets or sets the foreground colour of the dial |
| Value | double | Gets the value selected by the dial |

### Events

| Events | Description |
| -- | -- |
| ValueChanged | Fires whenever the dial value is changed |

### Examples

**XAML**

```xaml
<comentsys:Dial Height="200" Width="200" DialForeground="Blue" DialBackground="WhiteSmoke"/>
```

**C#**

```csharp
Dial dial = new Dial()
{
    Height = 200,
    Width = 200,
    DialBackground = new SolidColorBrush(Colors.WhiteSmoke),
    DialForeground= new SolidColorBrush(Colors.Blue),
};
```

## Segment

Segment control that can be used to display numbers like a seven-segment display plus colon character

### Syntax

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Segment Height="100" Foreground="Red" Source="Time"/>
```

### Output

![Segment Control](Resources/Segment.png)

### Types

**Sources**

| Value | Description |
| -- | -- |
| Value | Show the provided Value |
| Time | Show the current Time |
| Date | Show the current Date |
| TimeDate | Show the current Time & Date |

### Properties

| Property | Type | Description |
| -- | -- | -- |
| Foreground | Brush | Gets or sets the foreground colour of the segment |
| Source | Sources | Gets or sets the source of the segment of Value, Time, Date or TimeDate |
| Value | string | Gets the value shown by the segment |

### Examples

**XAML**

```xaml
<comentsys:Segment Height="100" Foreground="PaleVioletRed" Source="TimeDate"/>
```

**C#**

```csharp
Segment segment = new Segment()
{
    Height = 100,
    Foreground = new SolidColorBrush(Colors.PaleVioletRed),
    Source = Segment.Sources.TimeDate,
};
```