    $(document).ready(function(){
        $(".star").on("mouseover", function () {
            $(".star").slice(0, $(".star").index(this) + 1).removeClass("fa-star-o").addClass("fa-star");
        });

        $(".star").on("mouseout", function(){
            $(".star").slice(0, $(".star").index(this) + 1).removeClass("fa-star").addClass("fa-star-o");
        });

        $(".star").on("click", function(){
            jQuery.post("/Goods/RateProduct/", {
                productId: $(this).parent().attr("id").substr(3),
                stars: $(".star").index(this) + 1
            }, notice,"json");
        });
        
        for (let i = 1; i < 5; i++)
        {
            $("#input_" + i).on("mouseover", function () {
                $("#input_" + i).css("backgroundColor", "#b0eb97");
            });

            $("#input_" + i).on("mouseout", function () {
                $("#input_" + i).css("backgroundColor", "white");
            });
        };

        $(".btn_").on("mouseover", function () {
            $(".btn_").css("backgroundColor", "#00ced1").css("color", "white");
        });

        $(".btn_").on("mouseout", function () {
            $(".btn_").css("backgroundColor", "white").css("color", "black");
        });

        $("#user").on("mouseover", function () {
            $("#user").css("color", "#0a7c32");
        });

        $(".navbar-nav #user").on("mouseout", function () {
            $(".navbar-nav #user").css("color", "black");
        });

        if (document.title == "EditProduct") {
            $("#container").addClass("container-fluid").removeClass("container");
        }
        else {
            $("#container").addClass("container").removeClass("container-fluid");
        }

        $("table tr").on("click", function () {
            $(event.target.parentNode).fadeOut(500).fadeIn(500);
            jQuery.post("/Admin/GetProduct/", {
                productId: parseInt($(event.target.parentNode.childNodes[1]).text())
            }, apply, "json");
            document.getElementById("name").focus();
        });

        function notice(data){
            $("#star_rating, #star_message").fadeOut(500, function () {
                if (data.status == "already_rate") {
                    $("#star_rating").text("Вы уже голосовали за этот товар!");
                }
                else
                {
                    $("#star_rating").text("Рейтинг " + data.rate);
                    for (var i = 1; i <= 5; i++) {
                        if (i <= parseFloat(data.rate)) {
                            $("#star_" + i).removeClass("fa-star-o").addClass("fa-star");
                        }
                        else {
                            $("#star_" + i).removeClass("fa-star").addClass("fa-star-o");
                        }
                    }
                }
                
            }).fadeIn(1500);
        }

        function apply(data){
            //console.log(data);
            $("#id").val(data.id);
            $("#name").val(data.name);
            $("#price").val(data.price);
            $("#imgURL").val(data.imgURL);
            $("#description").val(data.description);
            $("a.RateId").attr("href", "/Admin/ClearRate/?rateId=" + data.rateId)
                .removeClass("displ-o").fadeOut(0).fadeIn(2000);//.removeClass("displ-o");

        }
    });