# Chat's Play Hue

This application aims to make Smart Lighting controllable by Twitch viewers spending channel points.

It's being developed live on stream saturdays 16:30 CEST on my [Twitch channel](https://www.twitch.tv/RotesWasser).

## Stream Log

### Planned for upcoming streams

* Think of a way to improve Hue HTTP Speeds
* Re-Think control of lights; maybe Groups are a valid concept to support?
* Consider supporting Hue Entertainment support
    * Hue EDK is not an option as it requires signing an NDA, so couldn't be done on stream
    * "Raw" DTLS connection and Entertainment Protocol over it
        * Protocol is dead simple, DTLS Support for C# doesn't seem to exist, so might need to wrap some C Library (eww)
* Light pattern repository
* Light pattern editor
* Connect to Twitch

### Completed

#### Stream 2020-07-04
Accessing Groups via HTTP API

Potentially designing a layer of abstraction over Lights

#### Stream 2020-06-27
Setting colors of lights via Hue HTTP API
    * Luminance calculated by the library we're using seems wrong, investigate further until next stream.

#### Stream 2020-06-20
Enumeration + control of lights (on / off)

#### Stream 2020-06-13
Hue Bridge discovery

#### Stream 2020-06-06
* Basic architecture for the (client-side?) app
* Define some interfaces