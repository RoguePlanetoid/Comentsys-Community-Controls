# Comentsys Community Controls

Comentsys Community Controls is a set of open-source Universal Windows Platform Controls available for developing Windows 10 applications with full documentation available and source code on [GitHub](https://github.com/RoguePlanetoid/Comentsys-Community-Controls/)

## Dice

Dice control that can display values that resembles a single Dice or Die face

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Dice Height="100" Width="100" Background="Red" Foreground="White" CornerRadius="10" Value="3"/>
```

## Clock

Clock control that resembles an analogue clock

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Clock Height="300" Width="300" RimBackground="WhiteSmoke" RimForeground="Black" SecondHandForeground="Blue"/>
```

## Dial

Dial control that can be used to set a value between a minimum and maximum value

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Dial Height="200" Width="200" DialForeground="WhiteSmoke" DialBackground="Teal"/>
```

## Segment

Segment control that can be used to display numbers like a seven-segment display plus colon character

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Segment Height="100" Foreground="Red" Source="Time"/>
```

## Card

Card control can be used to display various Playing Cards for Card-based games like Blackjack, Poker and more

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Card Width="120" Value="8"/>
```

## Direct

Direct control can be used as an on-screen Directional Pad for XAML-based games

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Direct Width="150" Foreground="Black"/>
```

## Domino

Domino control can be used to display various Domino tile values

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Domino Width="150" Value="2"/>
```

## Matrix

Matrix control that can be used to display numbers like a dot-matrix display plus colon, dash and forward-slash character

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Matrix Height="250" Foreground="Orange" Source="Time"/>
```

## Stack

Stack control is a stack-based chart that can be used to display a set of values in the form of horizontal or vertical bars

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Stack Height="300" Width="300"/>
```

## Donut

Donut control is a donut-based pie-like chart that can be used to display a set of values in the form of arc-like segments

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Donut Height="300" Width="300"/>
```

## Direct

Direct control can be used as an on-screen Directional Pad for XAML-based games

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Stick Radius="100" Width="250" Foreground="Black" Fill="WhiteSmoke"/>
```

## Split

Split control that can be used to display numbers like a flip clock or split-flip display including animation for the flipping when values are changed

```xaml
<Page ...
     xmlns:comentsys="using:Comentsys.Community.Controls"/>

<comentsys:Split Height="250" Foreground="White" Source="Time"/>
```