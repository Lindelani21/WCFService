function toggleTaskDetails(assistantId) {
    var taskDetails = document.getElementById(assistantId + '-tasks');
    if (taskDetails.style.display === 'none' || taskDetails.style.display === '') {
        taskDetails.style.display = 'block';
    } else {
        taskDetails.style.display = 'none';
    }
}

function provideGuidance(assistantId) {
    var feedback = document.getElementById('feedback-' + assistantId).value;
    alert('Guidance provided for ' + assistantId + ' with feedback: ' + feedback);
}

function goBack() {
    window.history.back();
}