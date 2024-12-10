
    function formatCurrency(value) {
        return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    }

    function updatePriceRange(type) {
        var minRange = document.getElementById('rangeInputMin');
    var maxRange = document.getElementById('rangeInputMax');
    var minValue = minRange.value;
    var maxValue = maxRange.value;

    if (type === 'Min') {
        document.getElementById('amountMin').textContent = formatCurrency(minValue);
        } else {
        document.getElementById('amountMax').textContent = formatCurrency(maxValue);
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        updatePriceRange('Min');
    updatePriceRange('Max');
    });
