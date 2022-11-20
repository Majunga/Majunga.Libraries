async function submitForm(elementId) {
    var form = document.getElementById(elementId);
    form.dispatchEvent(new Event('submit'));
}

async function streamAudio(elementId, streamReference) {
    const arrayBuffer = await streamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);

    var element = document.getElementById(elementId);
    const url = URL.createObjectURL(blob);

    element.src = url;
    element.parentElement.load();

    console.info("Ready!", element.src);
}
