export function initialize(element, instance) {
    const controller = new AbortController();

    function bind(event, handler) {
        element.addEventListener(event, handler, { signal: controller.signal });
    }

    bind("play", () => instance.invokeMethodAsync("HandlePlay"));
    bind("pause", () => instance.invokeMethodAsync("HandlePause"));
    bind("ended", () => instance.invokeMethodAsync("HandleEnded"));
    bind("timeupdate", () => instance.invokeMethodAsync("HandleTimeUpdate", element.currentTime));
    bind("loadedmetadata", () => instance.invokeMethodAsync("HandleLoadedMetadata"));
    bind("canplay", () => instance.invokeMethodAsync("HandleCanPlay"));
    bind("canplaythrough", () => instance.invokeMethodAsync("HandleCanPlayThrough"));
    bind("seeking", () => instance.invokeMethodAsync("HandleSeeking"));
    bind("seeked", () => instance.invokeMethodAsync("HandleSeeked"));
    bind("volumechange", () => instance.invokeMethodAsync("HandleVolumeChange"));
    bind("error", () => instance.invokeMethodAsync("HandleError"));
    bind("waiting", () => instance.invokeMethodAsync("HandleWaiting"));
    bind("stalled", () => instance.invokeMethodAsync("HandleStalled"));
    bind("ratechange", () => instance.invokeMethodAsync("HandleRateChange"));
    bind("durationchange", () => instance.invokeMethodAsync("HandleDurationChange"));

    return {
        play: () => element.play(),
        pause: () => element.pause(),
        setVolume: (value) => element.volume = value,
        getDuration: () => element.duration,
        getCurrentTime: () => element.currentTime,
        setCurrentTime: (value) => element.currentTime = value,
        dispose: () => controller.abort(),
    };
}