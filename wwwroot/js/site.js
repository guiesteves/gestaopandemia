// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



function FiltrarDropDown(context) {
    var value = $(context).val().toLowerCase();
    $(".dropdown-item").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function AtualizarValorDowpDown(context, campoValor, campoIdentificador) {
    $(campoValor).val($(context)[0].innerText.split('(')[0]);
    $(campoIdentificador).val($(context)[0].innerText.split('(')[1].replace(')',''));
}

function LimparPesquisa(campoPesquisa) {
    $(campoPesquisa).val("");
    FiltrarDropDown(campoPesquisa);
}
    




var modalVarianteAgentePatogenico = "#divVarianteAgentePatogenico";

window.onload = function () {
    $(modalVarianteAgentePatogenico).on('hide.bs.modal', function (e) {
        LimparModalVariante(modalVarianteAgentePatogenico);
    })
};

function LimparModalVariante(selector) {
    $(selector + ' input[type="text"],textarea').val("");
    $(selector + ' input[type="hidden"]').val("");
    $(selector + ' input[type="number"]').val("");
    $(selector + " select").val(-1);
    $(selector + ' input[type="radio"]').prop('checked', false);
    $("#posicaoGrid").val("-1");
}


function ExcluirVariante(context) {
    $(context).closest("tr").remove();
    RenomearAtrubutosNameInputs();
}

/**
 * Renomeia os elementos da tabela de variantes pois todos os novos estão com o valor zero. Ex:
 * De:                                              Para: 
 * name="ListaVarianteAgentePatogenicoViewModel[0].Nome" ->  name="ListaVarianteAgentePatogenicoViewModel[0].Nome"
 * name="ListaVarianteAgentePatogenicoViewModel[0].Nome" ->  name="ListaVarianteAgentePatogenicoViewModel[1].Nome"
 * name="ListaVarianteAgentePatogenicoViewModel[0].Nome" ->  name="ListaVarianteAgentePatogenicoViewModel[2].Nome"
 */
function RenomearAtrubutosNameInputs() {
    $("#tbodyVarianteAgentePatogenico tr").each(function (index, item) {
        item.id = "linha_" + index;
        $("input", item).each(function (indexInput, itemInput) {
            var nome = itemInput.name;
            nome = nome.replace(/[\d+]/, index);
            itemInput.name = nome;
        });
    });
}

function EditarVariante(context) {
    var tr = $(context).closest("tr")[0];
    var index = $(context).closest("tr")[0].id.split("_")[1];
    $("#posicaoGrid").val(index);
    $("#VarianteAgentePatogenicoId").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].VarianteAgentePatogenicoId']")[0].value);
    $("#AgentePatogenicoId").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].AgentePatogenicoId']")[0].value);
    $("#NomeVariante").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].Nome")[0].value);
    $("#PrincipaisMutacoes").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].PrincipaisMutacoes']")[0].value);
    $("#Caracteristica").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].Caracteristica']")[0].value);
    $("#PaisId").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].PaisId']")[0].value);
    $("#dropdownPais").val($("[name='ListaVarianteAgentePatogenicoViewModel[" + index + "].PaisNome']")[0].value);
    ShowModal(modalVarianteAgentePatogenico);
}

function ShowModal(selector) {
    $(selector).modal('show');
}

function IncluirAlterarVariante() {
    $("#formModal").validate().element("#VarianteAgentePatogenicoId");
    $("#formModal").validate().element("#NomeVariante");
    $("#formModal").validate().element("#PrincipaisMutacoes");
    $("#formModal").validate().element("#Caracteristica");
    $("#formModal").validate().element("#PaisId");

    if (!$("#formModal").valid()) {
        return false;
    }


    var posicaoGrid = $("#posicaoGrid").val()

    if (posicaoGrid >= 0)
    {
        $("#linha_" + posicaoGrid).remove();
    }

    var table = document.getElementById('tbodyVarianteAgentePatogenico');
    trLinha = table.insertRow(posicaoGrid);
    trLinha.classList.add("text-center");
    tdId = trLinha.insertCell(0);
    tdId.classList.add("fw-bold");
    tdNome = trLinha.insertCell(1);
    tdPrincipaisMutacoes = trLinha.insertCell(2);
    tdCaracteristica = trLinha.insertCell(3);
    tdPaisId = trLinha.insertCell(4);
    tdbotoes = trLinha.insertCell(5);
    tdbotoes.classList.add("text-end");


    console.log($("#VarianteAgentePatogenicoId").val());
    tdId.innerHTML = ($("#VarianteAgentePatogenicoId").val() === '0' || $("#VarianteAgentePatogenicoId").val() === '' ? "-" : $("#VarianteAgentePatogenicoId").val()) +
                     '<input name="ListaVarianteAgentePatogenicoViewModel[0].AgentePatogenicoId" type="text" hidden="" value="' + $("#AgentePatogenicoId").val() + '">' +
                     '<input name="ListaVarianteAgentePatogenicoViewModel[0].VarianteAgentePatogenicoId" type="text" hidden="" value="' + $("#VarianteAgentePatogenicoId").val() + '">';
    tdNome.innerHTML = $("#NomeVariante").val() + '<input name="ListaVarianteAgentePatogenicoViewModel[0].Nome" type="text" hidden="" value="' + $("#NomeVariante").val() + '">';
    tdPrincipaisMutacoes.innerHTML = $("#PrincipaisMutacoes").val() + '<input name="ListaVarianteAgentePatogenicoViewModel[0].PrincipaisMutacoes" type="text" hidden="" value="' + $("#PrincipaisMutacoes").val() + '">';
    tdCaracteristica.innerHTML = $("#Caracteristica").val() + '<input name="ListaVarianteAgentePatogenicoViewModel[0].Caracteristica" type="text" hidden="" value="' + $("#Caracteristica").val() + '">';
    tdPaisId.innerHTML = $("#dropdownPais").val() + '<input name="ListaVarianteAgentePatogenicoViewModel[0].PaisId" type="text" hidden="" value="' + $("#PaisId").val() + '">' +
                                                    '<input name="ListaVarianteAgentePatogenicoViewModel[0].PaisNome" type="text" hidden="" value="' + $("#dropdownPais").val() + '">';
    tdbotoes.innerHTML = ' <a href="#" onclick="EditarVariante(this)"><i class="bi bi-pencil-square text-primary m-1" style="font-size: 2rem" title="Editar"></i></a>'+
                          '<a href="#" onclick="ExcluirVariante(this)"><i class="bi bi-x-square text-danger" style="font-size: 2rem" title="Excluir"></i></a >';
    

    FacharModal(modalVarianteAgentePatogenico);
    LimparModalVariante(modalVarianteAgentePatogenico);
    RenomearAtrubutosNameInputs();

    function FacharModal(selector) {
        $(selector).modal('hide');
    }
}