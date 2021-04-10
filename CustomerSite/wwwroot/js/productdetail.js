  
$('input[name=rating]').on('change', function(e) {
    e.preventDefault();
    $(this).closest("form").submit();
});