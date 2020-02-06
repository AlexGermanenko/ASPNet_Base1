    $(document).ready(function(){
        $(".star").on("mouseover", function () {
            $(".star").slice(0, $(".star").index(this) + 1).removeClass("fa-star-o").addClass("fa-star");
        });
        $(".star").on("mouseout", function(){
            $(".star").slice(0, $(".star").index(this) + 1).removeClass("fa-star").addClass("fa-star-o");
        });
        $(".star").on("click", function(){
            jQuery.post("/Goods/Product/", {
                productId: $(this).parent().attr("id").substr(3),
                stars: $(".star").index(this) + 1 }, notice,"text" );
        });
        function notice(data){
            $("#star_rating, #star_votes, #star_message").fadeOut(500, function () {
                $("#star_rating").text(data.points);
                $("#star_votes").text(data.votes);
                $("#star_message").text(data.message);
            }).fadeIn(1500);
        }
    });