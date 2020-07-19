var objVendaMembroSOA = null;
var objVendaMembroOA = null;

var objArrayCompradores = new Array();
var dialogFullFormaPagamento = null;

function VendaPadrao() {
    window.location.href = rootURL + 'Recebedoria/Recebimento';
}

function LimpaTextoFiltroRecebedoria() {
    $("#txtItensSearch").val("");
    FiltroItens($("#txtItensSearch"));    
    $("#txtItensSearch").focus();
}

function SelecionaFiltroRecebedoria(OpcaoFiltro) {
    OpcaoFiltroUsuarioLogado = OpcaoFiltro;

    strFiltroText = "Recebedoria / Suprimentos";

    switch (OpcaoFiltro) {
        case 1:
            strFiltroText = "Recebedoria";
            break;
        case 2:
            strFiltroText = "Suprimentos";
            break;
    }

    $("#btnItensSearch").html("<i class=\"fa fa-search\"></i> " + strFiltroText);    
    FiltroItens($("#txtItensSearch"));

    $("#txtItensSearch").focus();

    $.ajax({
        type: 'POST',
        data: {
            OpcaoFiltro: OpcaoFiltro
        },
        url: rootURL + 'Recebedoria/Recebimento/GravarOpcaoFiltro',
        beforeSend: function () {
        },
        success: function (data) {
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Recebimento - Opção de Filtro', str);
        },
        complete: function () {
        }
    });
}

/* INÍCIO - COMPRADOR */
function ConfirmarData(data) {
    ClearNotify();

    $.ajax({
        type: 'POST',
        data: {
            Data: data
        },
        url: rootURL + 'Recebedoria/Recebimento/ConfirmarData',
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if (data.success) {
                $("#pnlSelecaoComprador").show();
            } else {
                ShowNotify('danger', 'Recebimento - Confirmar Data', data.msg);
            }

            
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Recebimento - Confirmar Data', str);            
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}


function SelecionaComprador(guid) {
    ClearNotify();

    $.ajax({
        type: 'POST',
        data: {
            guid: guid
        },
        url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorSelecionar',
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            dialogVisitante.dialog("close");
            $("#pnlVendaComprador").html(data);
            HabilitaItens();
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Recebimento - Visitante', str, 'frmConsultaVisitante');
            $("#frmConsultaVisitante #txtNomeVisitante").focus();
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function ConfiguraDialogVisitante() {
    $("#myModalAbrirVisitante button.btn-warning, #myModalAbrirVisitante button.btn-success").attr("disabled", "disabled");

    if (dialogVisitante == null) {
        dialogVisitante = $("#myModalVisitante").dialog({
            autoOpen: false,
            width: (thisDevice == "mobile") ? parseInt($(window).width()) : parseInt($("#content").css("width")) / 2,
            resizable: false,
            modal: true,
            closeOnEscape: true,
            position: { my: "center top", at: "center top", of: $("#content") }
        });
    }
}

function OpenDialogVisitante() {
    $.ajax({
        type: 'GET',
        url: rootURL + 'Recebedoria/Recebimento/Visitante',
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            OnCompleteForm();
            $("#myModalVisitante_Content").html(data);
            dialogVisitante.dialog("open");
            dialogVisitante.dialog("moveToTop");
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('danger', 'Recebimento - Visitante', str);            
        },
        complete: function () {
            
        }
    });    
}

function CloseDialogVisitante() {
    dialogVisitante.dialog("close");
}

function SelecionarVisitante(obj) {
    ClearNotify();

    var txtNomeVisitante = $("#frmConsultaVisitante #txtNomeVisitante").val().trim();

    if (txtNomeVisitante.length > 3) { // && txtNomeVisitante.split(" ").length > 1
        $.ajax({
            type: 'POST',
            data: {
                VisitanteNome: txtNomeVisitante,
                VisitanteGLP: $("#frmConsultaVisitante #txtGLPVisitante").val().trim(),
                MembroTOM: $("#frmConsultaVisitante #cbVisitanteTOM").is(":checked")
            },
            url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorVisitante',
            beforeSend: function () {
                dialogVisitante.dialog("close");
                OnBeginForm();
            },
            success: function (data) {
                $("#pnlVendaComprador").html(data);
                HabilitaItens();
            },
            error: function (xhr, st, str) {
                ShowNotify('danger', 'Recebimento - Visitante', str, 'frmConsultaVisitante');
                $("#frmConsultaVisitante #txtNomeVisitante").focus();
            },
            complete: function () {
                OnCompleteForm();
            }
        });
    } else {
        ShowNotify('danger', 'Recebimento - Visitante', 'Informe o Nome do Visitante.', 'frmConsultaVisitante');
        $("#frmConsultaVisitante #txtNomeVisitante").focus();
    }
}

function HabilitaAlterarNomeVisitante(guid) {
    $("#txtNomeVisitante_" + guid).show();
    $("#lblNomeVisitante_" + guid).hide();

    $("#txtNomeVisitante_" + guid).val($("#lblNomeVisitante_" + guid).text())

    $("#txtNomeVisitante_" + guid).focus();
}

function AlterarNomeVisitante(obj) {
    guid = $(obj).attr("id").replace("txtNomeVisitante_", "");

    $("#lblNomeVisitante_" + guid).show();
    $("#txtNomeVisitante_" + guid).hide();

    if ($("#txtNomeVisitante_" + guid).val().trim().length > 2 && $("#txtNomeVisitante_" + guid).val().trim().toUpperCase() != $("#lblNomeVisitante_" + guid).text()) {
        var txtNomeVisitante = $("#txtNomeVisitante_" + guid).val().trim().toUpperCase();
        $("#lblNomeVisitante_" + guid).text(txtNomeVisitante);

        $.ajax({
            type: 'POST',
            data: {
                VisitanteNome: txtNomeVisitante,
                guid: guid
            },
            url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorAlterarVisitante',
            beforeSend: function () {
                dialogVisitante.dialog("close");
                OnBeginForm();
            },
            success: function (data) {
                $("#pnlVendaComprador").html(data);
            },
            error: function (xhr, st, str) {
                ShowNotify('danger', 'Recebimento - Visitante', str, 'frmConsultaVisitante');
            },
            complete: function () {
                OnCompleteForm();
            }
        });
    }
}

function ExcluirComprador(nome, guid) {
    OpenDialogExcluir('Ao excluir o comprador: ' + nome + ', todos os itens dessa compra vinculados a ele, serão perdidos<br> Deseja continuar ?', 'ExcluirCompradorConfirmacao(\'' + guid + '\')');
}

function CancelarVenda(VendaId) {
    ClearNotify();

    dialogCancelarVenda.dialog("open");
    dialogCancelarVenda.dialog("moveToTop");

    $("#btnCancelarRecebedoriaim").unbind("click");
    $("#btnCancelarRecebedoriaim").bind('click', function () {
        CancelarVendaConfirmacao(VendaId);
    });
}

function CancelarVendaConfirmacao(VendaId) {
    $.ajax({
        type: 'POST',
        data: {
            VendaId: VendaId,
            Motivo: $("#txtCancelarVenda").val()
        },
        url: rootURL + 'Recebedoria/Recebimento/CancelarVenda',
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if (data.success) {
                $("#SituacaoVenda_" + VendaId).text("Cancelado");
                $("#btnCancelarVenda_" + VendaId).hide();
                $("#btnReciboVenda_" + VendaId).hide();

                dialogCancelarVenda.dialog("close");
            } else {
                ShowNotify('danger', 'Recebimento - Cancelar', data.msg, 'myModalCancelarVenda');
            }
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Recebimento - Visitante', str, 'frmConsultaVisitante');
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function ExcluirCompradorConfirmacao(guid) {
    $.ajax({
        type: 'POST',
        data: {
            guid: guid
        },
        url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorExcluir',
        beforeSend: function () {
            dialogVisitante.dialog("close");
            OnBeginForm();
        },
        success: function (data) {
            if ($("#pnlVendaComprador tbody tr[tkey='" + guid + "']").hasClass("txt-color-blueLight") == false) {
                $("#pnlVendaItens").html("");
            }

            $("#pnlVendaComprador").html(data);
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Recebimento - Visitante', str, 'frmConsultaVisitante');
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function GET_AssistenteComprador(refreshItens) {
    $.ajax({
        type: 'GET',
        url: rootURL + 'Recebedoria/Recebimento/AssistenteComprador',
        beforeSend: function () {
            $("#pnlVendaComprador").html("");
            ShowProgress('pnlVendaComprador');
        },
        success: function (data) {
            $("#pnlVendaComprador").html(data);

            if (refreshItens) {
                HabilitaItens();
            }
        },
        error: function (xhr, st, str) {

        },
        complete: function () {
            CloseDialogAbrirMembroGLP();
        }
    });
}

function OpenDialogAbrirMembroGLPVenda() {
    consultaMembroSelecionarFuncao = "SelecionarResultadoBuscaConsultaMembroVenda";
    OpenDialogAbrirMembroGLP(true);
}

function SelecionarResultadoBuscaConsultaMembro(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast) {
    ClearNotify();
    eval(consultaMembroSelecionarFuncao + "(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast)");
}

function SelecionarResultadoBuscaConsultaMembroVenda(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast) {
    ClearNotify();
    
    $.ajax({
        type: 'GET',
        data: { CodigoAfiliacao: Chave, NomeMembro: Nome, Completo: false, MembroOGG, fSeqCadast },
        url: rootURL + 'OrganismoAfiliado/Membro/ConsultaMembroVisaoGeralJSON',
        beforeSend: function () {
            CloseDialogAbrirMembroGLP();
            OnBeginForm();
        },
        success: function (data) {
            if (data.success) {
                objVendaMembroOA = JSON.parse(data.MembroOA);

                $.ajax({
                    type: 'POST',
                    data: {
                        MembroOA: objVendaMembroOA
                    },
                    url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorMembro',
                    beforeSend: function () {

                    },
                    success: function (data) {

                        if ($("#pnlVendaComprador").html() == undefined) {
                            ShowProgress('ListaMembro');
                            window.location.href = rootURL + 'Recebedoria/Recebimento';
                        } else {
                            $("#pnlVendaComprador").html(data);
                            HabilitaItens();
                        }                        
                    },
                    error: function (xhr, st, str) {
                        OnCompleteForm();
                        ShowNotify('danger', 'Falha no sistema', MsgNaoFoiPossivelCompletarOperacao, 'frmConsultaMembro');
                    },
                    complete: function () {
                        OnCompleteForm();
                    }
                });
            }

        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('danger', 'Falha no sistema', MsgNaoFoiPossivelCompletarOperacao, 'frmConsultaMembro');
        },
        complete: function () {
            CloseDialogAbrirMembroGLP();
        }
    });
}
/* FIM - COMPRADOR */

/* INÍCIO - ITENS */
function HabilitaItens() {
    ShowProgress('pnlVendaItens');

    $.ajax({
        type: 'GET',
        url: rootURL + 'Recebedoria/Recebimento/AssistenteItens',
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            $("#pnlVendaItens").html(data);
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

//accent-folding function
var accent_map = {
    'ẚ': 'a',
    'Á': 'a',
    'á': 'a',
    'À': 'a',
    'à': 'a',
    'Ă': 'a',
    'ă': 'a',
    'Ắ': 'a',
    'ắ': 'a',
    'Ằ': 'a',
    'ằ': 'a',
    'Ẵ': 'a',
    'ẵ': 'a',
    'Ẳ': 'a',
    'ẳ': 'a',
    'Â': 'a',
    'â': 'a',
    'Ấ': 'a',
    'ấ': 'a',
    'Ầ': 'a',
    'ầ': 'a',
    'Ẫ': 'a',
    'ẫ': 'a',
    'Ẩ': 'a',
    'ẩ': 'a',
    'Ǎ': 'a',
    'ǎ': 'a',
    'Å': 'a',
    'å': 'a',
    'Ǻ': 'a',
    'ǻ': 'a',
    'Ä': 'a',
    'ä': 'a',
    'Ǟ': 'a',
    'ǟ': 'a',
    'Ã': 'a',
    'ã': 'a',
    'Ȧ': 'a',
    'ȧ': 'a',
    'Ǡ': 'a',
    'ǡ': 'a',
    'Ą': 'a',
    'ą': 'a',
    'Ā': 'a',
    'ā': 'a',
    'Ả': 'a',
    'ả': 'a',
    'Ȁ': 'a',
    'ȁ': 'a',
    'Ȃ': 'a',
    'ȃ': 'a',
    'Ạ': 'a',
    'ạ': 'a',
    'Ặ': 'a',
    'ặ': 'a',
    'Ậ': 'a',
    'ậ': 'a',
    'Ḁ': 'a',
    'ḁ': 'a',
    'Ⱥ': 'a',
    'ⱥ': 'a',
    'Ǽ': 'a',
    'ǽ': 'a',
    'Ǣ': 'a',
    'ǣ': 'a',
    'Ḃ': 'b',
    'ḃ': 'b',
    'Ḅ': 'b',
    'ḅ': 'b',
    'Ḇ': 'b',
    'ḇ': 'b',
    'Ƀ': 'b',
    'ƀ': 'b',
    'ᵬ': 'b',
    'Ɓ': 'b',
    'ɓ': 'b',
    'Ƃ': 'b',
    'ƃ': 'b',
    'Ć': 'c',
    'ć': 'c',
    'Ĉ': 'c',
    'ĉ': 'c',
    'Č': 'c',
    'č': 'c',
    'Ċ': 'c',
    'ċ': 'c',
    'Ç': 'c',
    'ç': 'c',
    'Ḉ': 'c',
    'ḉ': 'c',
    'Ȼ': 'c',
    'ȼ': 'c',
    'Ƈ': 'c',
    'ƈ': 'c',
    'ɕ': 'c',
    'Ď': 'd',
    'ď': 'd',
    'Ḋ': 'd',
    'ḋ': 'd',
    'Ḑ': 'd',
    'ḑ': 'd',
    'Ḍ': 'd',
    'ḍ': 'd',
    'Ḓ': 'd',
    'ḓ': 'd',
    'Ḏ': 'd',
    'ḏ': 'd',
    'Đ': 'd',
    'đ': 'd',
    'ᵭ': 'd',
    'Ɖ': 'd',
    'ɖ': 'd',
    'Ɗ': 'd',
    'ɗ': 'd',
    'Ƌ': 'd',
    'ƌ': 'd',
    'ȡ': 'd',
    'ð': 'd',
    'É': 'e',
    'Ə': 'e',
    'Ǝ': 'e',
    'ǝ': 'e',
    'é': 'e',
    'È': 'e',
    'è': 'e',
    'Ĕ': 'e',
    'ĕ': 'e',
    'Ê': 'e',
    'ê': 'e',
    'Ế': 'e',
    'ế': 'e',
    'Ề': 'e',
    'ề': 'e',
    'Ễ': 'e',
    'ễ': 'e',
    'Ể': 'e',
    'ể': 'e',
    'Ě': 'e',
    'ě': 'e',
    'Ë': 'e',
    'ë': 'e',
    'Ẽ': 'e',
    'ẽ': 'e',
    'Ė': 'e',
    'ė': 'e',
    'Ȩ': 'e',
    'ȩ': 'e',
    'Ḝ': 'e',
    'ḝ': 'e',
    'Ę': 'e',
    'ę': 'e',
    'Ē': 'e',
    'ē': 'e',
    'Ḗ': 'e',
    'ḗ': 'e',
    'Ḕ': 'e',
    'ḕ': 'e',
    'Ẻ': 'e',
    'ẻ': 'e',
    'Ȅ': 'e',
    'ȅ': 'e',
    'Ȇ': 'e',
    'ȇ': 'e',
    'Ẹ': 'e',
    'ẹ': 'e',
    'Ệ': 'e',
    'ệ': 'e',
    'Ḙ': 'e',
    'ḙ': 'e',
    'Ḛ': 'e',
    'ḛ': 'e',
    'Ɇ': 'e',
    'ɇ': 'e',
    'ɚ': 'e',
    'ɝ': 'e',
    'Ḟ': 'f',
    'ḟ': 'f',
    'ᵮ': 'f',
    'Ƒ': 'f',
    'ƒ': 'f',
    'Ǵ': 'g',
    'ǵ': 'g',
    'Ğ': 'g',
    'ğ': 'g',
    'Ĝ': 'g',
    'ĝ': 'g',
    'Ǧ': 'g',
    'ǧ': 'g',
    'Ġ': 'g',
    'ġ': 'g',
    'Ģ': 'g',
    'ģ': 'g',
    'Ḡ': 'g',
    'ḡ': 'g',
    'Ǥ': 'g',
    'ǥ': 'g',
    'Ɠ': 'g',
    'ɠ': 'g',
    'Ĥ': 'h',
    'ĥ': 'h',
    'Ȟ': 'h',
    'ȟ': 'h',
    'Ḧ': 'h',
    'ḧ': 'h',
    'Ḣ': 'h',
    'ḣ': 'h',
    'Ḩ': 'h',
    'ḩ': 'h',
    'Ḥ': 'h',
    'ḥ': 'h',
    'Ḫ': 'h',
    'ḫ': 'h',
    'H': 'h',
    '̱': 'h',
    'ẖ': 'h',
    'Ħ': 'h',
    'ħ': 'h',
    'Ⱨ': 'h',
    'ⱨ': 'h',
    'Í': 'i',
    'í': 'i',
    'Ì': 'i',
    'ì': 'i',
    'Ĭ': 'i',
    'ĭ': 'i',
    'Î': 'i',
    'î': 'i',
    'Ǐ': 'i',
    'ǐ': 'i',
    'Ï': 'i',
    'ï': 'i',
    'Ḯ': 'i',
    'ḯ': 'i',
    'Ĩ': 'i',
    'ĩ': 'i',
    'İ': 'i',
    'i': 'i',
    'Į': 'i',
    'į': 'i',
    'Ī': 'i',
    'ī': 'i',
    'Ỉ': 'i',
    'ỉ': 'i',
    'Ȉ': 'i',
    'ȉ': 'i',
    'Ȋ': 'i',
    'ȋ': 'i',
    'Ị': 'i',
    'ị': 'i',
    'Ḭ': 'i',
    'ḭ': 'i',
    'I': 'i',
    'ı': 'i',
    'Ɨ': 'i',
    'ɨ': 'i',
    'Ĵ': 'j',
    'ĵ': 'j',
    'J': 'j',
    '̌': 'j',
    'ǰ': 'j',
    'ȷ': 'j',
    'Ɉ': 'j',
    'ɉ': 'j',
    'ʝ': 'j',
    'ɟ': 'j',
    'ʄ': 'j',
    'Ḱ': 'k',
    'ḱ': 'k',
    'Ǩ': 'k',
    'ǩ': 'k',
    'Ķ': 'k',
    'ķ': 'k',
    'Ḳ': 'k',
    'ḳ': 'k',
    'Ḵ': 'k',
    'ḵ': 'k',
    'Ƙ': 'k',
    'ƙ': 'k',
    'Ⱪ': 'k',
    'ⱪ': 'k',
    'Ĺ': 'a',
    'ĺ': 'l',
    'Ľ': 'l',
    'ľ': 'l',
    'Ļ': 'l',
    'ļ': 'l',
    'Ḷ': 'l',
    'ḷ': 'l',
    'Ḹ': 'l',
    'ḹ': 'l',
    'Ḽ': 'l',
    'ḽ': 'l',
    'Ḻ': 'l',
    'ḻ': 'l',
    'Ł': 'l',
    'ł': 'l',
    'Ł': 'l',
    '̣': 'l',
    'ł': 'l',
    '̣': 'l',
    'Ŀ': 'l',
    'ŀ': 'l',
    'Ƚ': 'l',
    'ƚ': 'l',
    'Ⱡ': 'l',
    'ⱡ': 'l',
    'Ɫ': 'l',
    'ɫ': 'l',
    'ɬ': 'l',
    'ɭ': 'l',
    'ȴ': 'l',
    'Ḿ': 'm',
    'ḿ': 'm',
    'Ṁ': 'm',
    'ṁ': 'm',
    'Ṃ': 'm',
    'ṃ': 'm',
    'ɱ': 'm',
    'Ń': 'n',
    'ń': 'n',
    'Ǹ': 'n',
    'ǹ': 'n',
    'Ň': 'n',
    'ň': 'n',
    'Ñ': 'n',
    'ñ': 'n',
    'Ṅ': 'n',
    'ṅ': 'n',
    'Ņ': 'n',
    'ņ': 'n',
    'Ṇ': 'n',
    'ṇ': 'n',
    'Ṋ': 'n',
    'ṋ': 'n',
    'Ṉ': 'n',
    'ṉ': 'n',
    'Ɲ': 'n',
    'ɲ': 'n',
    'Ƞ': 'n',
    'ƞ': 'n',
    'ɳ': 'n',
    'ȵ': 'n',
    'N': 'n',
    '̈': 'n',
    'n': 'n',
    '̈': 'n',
    'Ó': 'o',
    'ó': 'o',
    'Ò': 'o',
    'ò': 'o',
    'Ŏ': 'o',
    'ŏ': 'o',
    'Ô': 'o',
    'ô': 'o',
    'Ố': 'o',
    'ố': 'o',
    'Ồ': 'o',
    'ồ': 'o',
    'Ỗ': 'o',
    'ỗ': 'o',
    'Ổ': 'o',
    'ổ': 'o',
    'Ǒ': 'o',
    'ǒ': 'o',
    'Ö': 'o',
    'ö': 'o',
    'Ȫ': 'o',
    'ȫ': 'o',
    'Ő': 'o',
    'ő': 'o',
    'Õ': 'o',
    'õ': 'o',
    'Ṍ': 'o',
    'ṍ': 'o',
    'Ṏ': 'o',
    'ṏ': 'o',
    'Ȭ': 'o',
    'ȭ': 'o',
    'Ȯ': 'o',
    'ȯ': 'o',
    'Ȱ': 'o',
    'ȱ': 'o',
    'Ø': 'o',
    'ø': 'o',
    'Ǿ': 'o',
    'ǿ': 'o',
    'Ǫ': 'o',
    'ǫ': 'o',
    'Ǭ': 'o',
    'ǭ': 'o',
    'Ō': 'o',
    'ō': 'o',
    'Ṓ': 'o',
    'ṓ': 'o',
    'Ṑ': 'o',
    'ṑ': 'o',
    'Ỏ': 'o',
    'ỏ': 'o',
    'Ȍ': 'o',
    'ȍ': 'o',
    'Ȏ': 'o',
    'ȏ': 'o',
    'Ơ': 'o',
    'ơ': 'o',
    'Ớ': 'o',
    'ớ': 'o',
    'Ờ': 'o',
    'ờ': 'o',
    'Ỡ': 'o',
    'ỡ': 'o',
    'Ở': 'o',
    'ở': 'o',
    'Ợ': 'o',
    'ợ': 'o',
    'Ọ': 'o',
    'ọ': 'o',
    'Ộ': 'o',
    'ộ': 'o',
    'Ɵ': 'o',
    'ɵ': 'o',
    'Ṕ': 'p',
    'ṕ': 'p',
    'Ṗ': 'p',
    'ṗ': 'p',
    'Ᵽ': 'p',
    'Ƥ': 'p',
    'ƥ': 'p',
    'P': 'p',
    '̃': 'p',
    'p': 'p',
    '̃': 'p',
    'ʠ': 'q',
    'Ɋ': 'q',
    'ɋ': 'q',
    'Ŕ': 'r',
    'ŕ': 'r',
    'Ř': 'r',
    'ř': 'r',
    'Ṙ': 'r',
    'ṙ': 'r',
    'Ŗ': 'r',
    'ŗ': 'r',
    'Ȑ': 'r',
    'ȑ': 'r',
    'Ȓ': 'r',
    'ȓ': 'r',
    'Ṛ': 'r',
    'ṛ': 'r',
    'Ṝ': 'r',
    'ṝ': 'r',
    'Ṟ': 'r',
    'ṟ': 'r',
    'Ɍ': 'r',
    'ɍ': 'r',
    'ᵲ': 'r',
    'ɼ': 'r',
    'Ɽ': 'r',
    'ɽ': 'r',
    'ɾ': 'r',
    'ᵳ': 'r',
    'ß': 's',
    'Ś': 's',
    'ś': 's',
    'Ṥ': 's',
    'ṥ': 's',
    'Ŝ': 's',
    'ŝ': 's',
    'Š': 's',
    'š': 's',
    'Ṧ': 's',
    'ṧ': 's',
    'Ṡ': 's',
    'ṡ': 's',
    'ẛ': 's',
    'Ş': 's',
    'ş': 's',
    'Ṣ': 's',
    'ṣ': 's',
    'Ṩ': 's',
    'ṩ': 's',
    'Ș': 's',
    'ș': 's',
    'ʂ': 's',
    'S': 's',
    '̩': 's',
    's': 's',
    '̩': 's',
    'Þ': 't',
    'þ': 't',
    'Ť': 't',
    'ť': 't',
    'T': 't',
    '̈': 't',
    'ẗ': 't',
    'Ṫ': 't',
    'ṫ': 't',
    'Ţ': 't',
    'ţ': 't',
    'Ṭ': 't',
    'ṭ': 't',
    'Ț': 't',
    'ț': 't',
    'Ṱ': 't',
    'ṱ': 't',
    'Ṯ': 't',
    'ṯ': 't',
    'Ŧ': 't',
    'ŧ': 't',
    'Ⱦ': 't',
    'ⱦ': 't',
    'ᵵ': 't',
    'ƫ': 't',
    'Ƭ': 't',
    'ƭ': 't',
    'Ʈ': 't',
    'ʈ': 't',
    'ȶ': 't',
    'Ú': 'u',
    'ú': 'u',
    'Ù': 'u',
    'ù': 'u',
    'Ŭ': 'u',
    'ŭ': 'u',
    'Û': 'u',
    'û': 'u',
    'Ǔ': 'u',
    'ǔ': 'u',
    'Ů': 'u',
    'ů': 'u',
    'Ü': 'u',
    'ü': 'u',
    'Ǘ': 'u',
    'ǘ': 'u',
    'Ǜ': 'u',
    'ǜ': 'u',
    'Ǚ': 'u',
    'ǚ': 'u',
    'Ǖ': 'u',
    'ǖ': 'u',
    'Ű': 'u',
    'ű': 'u',
    'Ũ': 'u',
    'ũ': 'u',
    'Ṹ': 'u',
    'ṹ': 'u',
    'Ų': 'u',
    'ų': 'u',
    'Ū': 'u',
    'ū': 'u',
    'Ṻ': 'u',
    'ṻ': 'u',
    'Ủ': 'u',
    'ủ': 'u',
    'Ȕ': 'u',
    'ȕ': 'u',
    'Ȗ': 'u',
    'ȗ': 'u',
    'Ư': 'u',
    'ư': 'u',
    'Ứ': 'u',
    'ứ': 'u',
    'Ừ': 'u',
    'ừ': 'u',
    'Ữ': 'u',
    'ữ': 'u',
    'Ử': 'u',
    'ử': 'u',
    'Ự': 'u',
    'ự': 'u',
    'Ụ': 'u',
    'ụ': 'u',
    'Ṳ': 'u',
    'ṳ': 'u',
    'Ṷ': 'u',
    'ṷ': 'u',
    'Ṵ': 'u',
    'ṵ': 'u',
    'Ʉ': 'u',
    'ʉ': 'u',
    'Ṽ': 'v',
    'ṽ': 'v',
    'Ṿ': 'v',
    'ṿ': 'v',
    'Ʋ': 'v',
    'ʋ': 'v',
    'Ẃ': 'w',
    'ẃ': 'w',
    'Ẁ': 'w',
    'ẁ': 'w',
    'Ŵ': 'w',
    'ŵ': 'w',
    'W': 'w',
    '̊': 'w',
    'ẘ': 'w',
    'Ẅ': 'w',
    'ẅ': 'w',
    'Ẇ': 'w',
    'ẇ': 'w',
    'Ẉ': 'w',
    'ẉ': 'w',
    'Ẍ': 'x',
    'ẍ': 'x',
    'Ẋ': 'x',
    'ẋ': 'x',
    'Ý': 'y',
    'ý': 'y',
    'Ỳ': 'y',
    'ỳ': 'y',
    'Ŷ': 'y',
    'ŷ': 'y',
    'Y': 'y',
    '̊': 'y',
    'ẙ': 'y',
    'Ÿ': 'y',
    'ÿ': 'y',
    'Ỹ': 'y',
    'ỹ': 'y',
    'Ẏ': 'y',
    'ẏ': 'y',
    'Ȳ': 'y',
    'ȳ': 'y',
    'Ỷ': 'y',
    'ỷ': 'y',
    'Ỵ': 'y',
    'ỵ': 'y',
    'ʏ': 'y',
    'Ɏ': 'y',
    'ɏ': 'y',
    'Ƴ': 'y',
    'ƴ': 'y',
    'Ź': 'z',
    'ź': 'z',
    'Ẑ': 'z',
    'ẑ': 'z',
    'Ž': 'z',
    'ž': 'z',
    'Ż': 'z',
    'ż': 'z',
    'Ẓ': 'z',
    'ẓ': 'z',
    'Ẕ': 'z',
    'ẕ': 'z',
    'Ƶ': 'z',
    'ƶ': 'z',
    'Ȥ': 'z',
    'ȥ': 'z',
    'ʐ': 'z',
    'ʑ': 'z',
    'Ⱬ': 'z',
    'ⱬ': 'z',
    'Ǯ': 'z',
    'ǯ': 'z',
    'ƺ': 'z',

    // Roman fullwidth ascii equivalents: 0xff00 to 0xff5e
    '２': '2',
    '６': '6',
    'Ｂ': 'B',
    'Ｆ': 'F',
    'Ｊ': 'J',
    'Ｎ': 'N',
    'Ｒ': 'R',
    'Ｖ': 'V',
    'Ｚ': 'Z',
    'ｂ': 'b',
    'ｆ': 'f',
    'ｊ': 'j',
    'ｎ': 'n',
    'ｒ': 'r',
    'ｖ': 'v',
    'ｚ': 'z',
    '１': '1',
    '５': '5',
    '９': '9',
    'Ａ': 'A',
    'Ｅ': 'E',
    'Ｉ': 'I',
    'Ｍ': 'M',
    'Ｑ': 'Q',
    'Ｕ': 'U',
    'Ｙ': 'Y',
    'ａ': 'a',
    'ｅ': 'e',
    'ｉ': 'i',
    'ｍ': 'm',
    'ｑ': 'q',
    'ｕ': 'u',
    'ｙ': 'y',
    '０': '0',
    '４': '4',
    '８': '8',
    'Ｄ': 'D',
    'Ｈ': 'H',
    'Ｌ': 'L',
    'Ｐ': 'P',
    'Ｔ': 'T',
    'Ｘ': 'X',
    'ｄ': 'd',
    'ｈ': 'h',
    'ｌ': 'l',
    'ｐ': 'p',
    'ｔ': 't',
    'ｘ': 'x',
    '３': '3',
    '７': '7',
    'Ｃ': 'C',
    'Ｇ': 'G',
    'Ｋ': 'K',
    'Ｏ': 'O',
    'Ｓ': 'S',
    'Ｗ': 'W',
    'ｃ': 'c',
    'ｇ': 'g',
    'ｋ': 'k',
    'ｏ': 'o',
    'ｓ': 's',
    'ｗ': 'w'
};

var accentMap = {
    'á': 'a', 'é': 'e', 'í': 'i', 'ó': 'o', 'ú': 'u'
};

function accent_fold(s) {
    if (!s) { return ''; }
    var ret = '';
    for (var i = 0; i < s.length; i++) {
        ret += accent_map[s.charAt(i)] || s.charAt(i);
    }
    return ret;
};

function FiltroItens(objText) {
    var textToSearch = $(objText).val().toLowerCase().trim();
    var totalItens = 0;

    var blnShow = false;

    $("#pnlVendaItens tbody tr").hide();

    if (textToSearch.length > 0) {
        textToSearch = accent_fold(textToSearch);
        // td:nth-child(3)        
        $("#pnlVendaItens tbody tr").each(function (index) {
            blnShow = false;

            if (accent_fold($(this).text().toLowerCase()).includes(textToSearch) || accent_fold($($(this).find("td")[0]).text().toLowerCase()).includes(textToSearch) || accent_fold($($(this).find("td")[1]).text().toLowerCase()).includes(textToSearch)) {
                if (OpcaoFiltroUsuarioLogado != 0) {
                    switch (OpcaoFiltroUsuarioLogado) {
                        case 1:
                            blnShow = $(this).attr("suprimento") == "false";
                            break;
                        case 2:
                            blnShow = $(this).attr("suprimento") == "true";
                            break;
                    }
                } else {
                    blnShow = true;
                }         

                if (blnShow) {
                    $(this).show();
                    totalItens++;
                }
            }
        });
    } else {
        if (OpcaoFiltroUsuarioLogado == 0) {
            $("#pnlVendaItens tbody tr").show();
            totalItens = $("#pnlVendaItens tbody tr td:nth-child(3)").length;
        } else {
            $("#pnlVendaItens tbody tr").each(function (index) {
                blnShow = false;

                if (OpcaoFiltroUsuarioLogado != 0) {
                    switch (OpcaoFiltroUsuarioLogado) {
                        case 1:
                            blnShow = $(this).attr("suprimento") == "false";
                            break;
                        case 2:
                            blnShow = $(this).attr("suprimento") == "true";
                            break;
                    }
                } else {
                    blnShow = true;
                }

                if (blnShow) {
                    $(this).show();
                    totalItens++;
                }
            });
        }
    }

    if (totalItens > 0) {
        $("#pnlVendaItens tfoot tr td").text(totalItens + " ite" + ((totalItens > 1) ? "ns" : "m"));
    }
    else {
        $("#pnlVendaItens tfoot tr td").text("Não foi encontrado nenhum.");
    }
}

function AdicionarItem(obj, id) {
    var objId = $($($(obj).parent().parent().parent().find("td")[0]).find("input")[0]);
    var objQtde = $($($(obj).parent().parent().parent().find("td")[3]).find("input")[0]);
    var txtQtde = objQtde.val().trim();
    var txtValor = "";
    var blnAdicionar = false;

    objQtde.css("background-color", "white");
    objQtde.css("border-color", "initial");
    objQtde.css("border-width", "2px");

    if (txtQtde == "") {
        objQtde.css("background-color", "#fff0f0");
        objQtde.css("border-color", "#A90329");
        objQtde.css("border-width", "1px");
    }

    if ($($(obj).parent().parent().parent().find("td")[4]).find("input").length > 0) {
        var objValor = $($($(obj).parent().parent().parent().find("td")[4]).find("input")[0]);
        txtValor = objValor.val().trim();

        objValor.css("background-color", "white");
        objValor.css("border-color", "initial");
        objValor.css("border-width", "2px");

        if (txtValor == "") {
            objValor.css("background-color", "#fff0f0");
            objValor.css("border-color", "#A90329");
            objValor.css("border-width", "1px");
        } else {
            if (txtQtde != "") {
                blnAdicionar = true;
            }
        }
    } else {
        if (txtQtde != "") {
            txtValor = $($(obj).parent().parent().parent().find("td")[4]).text().trim();
            blnAdicionar = true;
        }
    }

    if (blnAdicionar) {
        var decValor = txtValor.replace(".", ",");
        var intQuantidade = parseInt(txtQtde);

        $.ajax({
            type: 'POST',
            data: {
                Id: parseInt(objId.val()),
                Quantidade: intQuantidade,
                Valor: decValor
            },
            url: rootURL + 'Recebedoria/Recebimento/AssistenteItensAdicionar',
            beforeSend: function () {
                OnBeginForm();
            },
            success: function (data) {
                if (data.success) {

                    $("#txtItensSearch").val("");
                    FiltroItens($("#txtItensSearch"));

                    $(obj).parent().parent().parent().remove();

                    var comprador = data.comprador;
                    var item = data.item;


                    if (comprador.ListaItens.length == 1) {
                        $.ajax({
                            type: 'POST',
                            url: rootURL + 'Recebedoria/Recebimento/AssistenteItensMembro',
                            data: { Comprador: comprador },
                            beforeSend: function () {
                                OnBeginForm();
                            },
                            success: function (data) {
                                $($("#pnlVendaComprador tbody tr[tkey='" + comprador.GUID + "']").next().find("td")[1]).html(data);
                                $("#pnlVendaComprador tbody tr[tkey='" + comprador.GUID + "']").next().css("display", "");
                            },
                            error: function (xhr, st, str) {
                                OnCompleteForm();
                                ShowError(MsgNaoFoiPossivelCompletarOperacao);
                            },
                            complete: function () {
                                OnCompleteForm();
                                CalculateItem();
                                $("#txtItensSearch").focus();
                            }
                        });
                    } else {
                        $.ajax({
                            type: 'POST',
                            url: rootURL + 'Recebedoria/Recebimento/AssistenteItensMembroItem',
                            data: { Item: item },
                            beforeSend: function () {
                                OnBeginForm();
                            },
                            success: function (data) {
                                $($($("#pnlVendaComprador tbody tr[tkey='" + comprador.GUID + "']").next().find("td")[1]).find("table tbody tr")[$($("#pnlVendaComprador tbody tr[tkey='" + comprador.GUID + "']").next().find("td")[1]).find("table tbody tr").length - 1]).after(data);
                                $($("#pnlVendaComprador tbody tr[tkey='" + comprador.GUID + "']").next().find("td")[1]).find("table tfoot tr td").html(comprador.ListaItens.length + " ite" + (comprador.ListaItens.length > 1 ? "ns" : "m"));
                            },
                            error: function (xhr, st, str) {
                                OnCompleteForm();
                                ShowError(MsgNaoFoiPossivelCompletarOperacao);
                            },
                            complete: function () {
                                OnCompleteForm();
                                CalculateItem();
                                $("#txtItensSearch").focus();
                            }
                        });
                    }

                } else {
                    ShowError(data.msg);
                }
            },
            error: function (xhr, st, str) {
                OnCompleteForm();
                ShowError(MsgNaoFoiPossivelCompletarOperacao);
            },
            complete: function () {

            }
        });
    }
}

function RemoverItem(obj, idItem) {
    var guid = $($($(obj).parent().parent().parent().parent().parent().parent().parent()[0]).prev()[0]).attr("tkey");

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/AssistenteItensRemover',
        data: { GUID: guid, IdItem: idItem },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if ($(obj).parent().parent().parent().parent().find("tr").length <= 1) {
                $("#pnlVendaComprador tbody tr[tkey='" + guid + "']").next().find("td").html("");
                $("#pnlVendaComprador tbody tr[tkey='" + guid + "']").next().css("display", "none");
            }

            $(obj).parent().parent().parent().remove();

            if (data != "") {
                $("#pnlVendaItens table tbody tr:first").before(data);
            }

            CalculateItem();

            SelecionaComprador(guid);
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function CalculateItem() {
    var decFinalTotal = 0;
    $("#pnlVendaComprador table tbody tr").each(function (index) {
        var attr = $(this).attr('tkey');
        if (typeof attr !== typeof undefined && attr !== false) {
            var lineItems = $($($(this).next().find("td")[1]).find("table tbody tr"));
            if (lineItems.length > 0) {
                for (var i = 0; i < lineItems.length; i++) {
                    var columnItems = $($(lineItems[i]).find("td"));
                    //console.log(columnItems);
                    decFinalTotal += parseFloat($(columnItems[4]).text().replace(",", "."));

                    //decFinalTotal
                    //if (columnItems.length == 7) {
                    //    if (parseInt($(columnItems[3]).children(0).val()) != NaN) {
                    //        decSubTotalQtde += parseInt($(columnItems[3]).children(0).val());
                    //        decFinalQtde += parseInt($(columnItems[3]).children(0).val());
                    //    } else {

                    //    }
                    //}
                    //for (var iCol = 0; iCol < columnItems.length; iCol++) {
                    //    if ($(columnItems[iCol]))
                    //    console.log(columnItems[iCol]);
                    //}
                }
            }
        }
    });
    
    $("#lblTotalFinal").text(decFinalTotal.toFixed(2).replace(".", ","));
}

function UpdateItem(obj, idItem) {
    var qtde = 0;
    var valor = 0;

    $($($(obj).parent().parent().children()[2]).find("input")[0]).css("background-color", "white");
    $($($(obj).parent().parent().children()[2]).find("input")[0]).css("border-color", "initial");
    $($($(obj).parent().parent().children()[2]).find("input")[0]).css("border-width", "2px");

    if (parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val()) != NaN && parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val()) > 0) {
        qtde = parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val());
    } else {
        $($($(obj).parent().parent().children()[2]).find("input")[0]).css("background-color", "#fff0f0");
        $($($(obj).parent().parent().children()[2]).find("input")[0]).css("border-color", "#A90329");
        $($($(obj).parent().parent().children()[2]).find("input")[0]).css("border-width", "1px");
    }

    if ($($(obj).parent().parent().children()[3]).find("input").length > 0) {

        $($($(obj).parent().parent().children()[3]).find("input")[0]).css("background-color", "white");
        $($($(obj).parent().parent().children()[3]).find("input")[0]).css("border-color", "initial");
        $($($(obj).parent().parent().children()[3]).find("input")[0]).css("border-width", "2px");

        if (parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val()) != NaN && parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val()) > 0) {
            valor = parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val().replace(",", "."));
        } else {
            $($($(obj).parent().parent().children()[3]).find("input")[0]).css("background-color", "#fff0f0");
            $($($(obj).parent().parent().children()[3]).find("input")[0]).css("border-color", "#A90329");
            $($($(obj).parent().parent().children()[3]).find("input")[0]).css("border-width", "1px");
        }
    } else {
        valor = parseFloat($($(obj).parent().parent().children()[3]).text().replace(",", "."));
    }

    if (qtde > 0 && valor > 0) {
        var total = qtde * valor;
        $($(obj).parent().parent().children()[4]).text(total.toFixed(2).replace(".", ","))
    } else {
        $($(obj).parent().parent().children()[4]).text(parseFloat("0").toFixed(2).replace(".", ","))
    }

    CalculateItem();
}

function UpdateItemGravar(obj, idItem) {
    var guid = $($($(obj).parent().parent().parent().parent().parent().parent()[0]).prev()[0]).attr("tkey");

    var qtde = 0;
    var valor = 0;

    if (parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val()) != NaN && parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val()) > 0) {
        qtde = parseInt($($($(obj).parent().parent().children()[2]).find("input")[0]).val());
    }

    if ($($(obj).parent().parent().children()[3]).find("input").length > 0) {

        if (parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val()) != NaN && parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val()) > 0) {
            valor = parseFloat($($($(obj).parent().parent().children()[3]).find("input")[0]).val().replace(",", "."));
        }
    } else {
        valor = parseFloat($($(obj).parent().parent().children()[3]).text().replace(",", "."));
    }

    var valorStr = valor.toString().replace(".", ",");

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/AssistenteItensAtualizar',
        data: { GUID: guid, IdItem: idItem, Quantidade: qtde, Valor: valorStr },
        beforeSend: function () {

        },
        success: function (data) {

        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {

        }
    });
}
/* FIM - ITENS */

/* INICIO - PAGAMENTO */
function AbrirPagamento() {
    ClearNotify();

    $.ajax({
        type: 'GET',
        url: rootURL + 'Recebedoria/Recebimento/ListarFormaPagamento',
        data: { Limpar: true },
        beforeSend: function () {

        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg);
            } else {
                if (dialogFullFormaPagamento == null) {

                    $(window).resize(function () {
                        ConfigureDialogFullFormaPagamento();
                    });

                    ConfigureDialogFullFormaPagamento();
                }

                $("#myModalFullFormaPagamentoContent").html(data);

                $("body").css({ overflow: 'hidden' });
                dialogFullFormaPagamento.dialog("open");
            }
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {

        }
    });
}

function GerarOrdemVenda() {
    OpenDialogExcluir("Deseja realmente gerar uma Ordem de Venda?<br /><br />Essa venda irá aparecer no menu Recebedoria / Ordem de Venda, para ser efetivada posteriormente.", "GerarOrdemVendaConfirmar()")
}

function GerarOrdemVendaConfirmar() {
    ClearNotify();

    $.ajax({
        type: 'GET',
        url: rootURL + 'Recebedoria/Recebimento/GerarOrdemVenda',
        data: { Limpar: true },
        beforeSend: function () {

        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg);
            } else {
                ShowNotify('success', 'Recebedoria', 'Ordem de Venda Gerada com sucesso: ' + data.NumeroOrdemVenda);


                OrdemVendaEmitirRecibo(data.OrdemVendaId);

                $("#pnlVendaComprador").html("");
                $("#pnlVendaItens").html("");
            }
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {

        }
    });
}

function ConfigureDialogFullFormaPagamento() {
    dialogFullFormaPagamento = $("#myModalFullFormaPagamento").dialog({
        autoOpen: false,
        width: parseInt($(window).width()),
        height: parseInt($(window).height()),
        resizable: false,
        draggable: false,
        closeOnEscape: true,
        modal: true,
        position: { my: "top top", at: "top top", of: window },
        beforeClose: function (event, ui) {
            $("body").css({ overflow: 'inherit' })
        }
    });
}

function AdicionaFormaPagamento(objButton, id) {
    var valor = $($(objButton).parent().parent().find("input")[0]).val();
    var nome = $(objButton).text();
    var observacao = $($(objButton).parent().parent().find("textarea")[0]).val();
    
    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/AdicionaFormaPagamento',
        data: { FormaPagamentoId: id, FormaPagamentoNome: nome, Valor: valor, Observacao: observacao },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg, 'myModalFullFormaPagamentoContent');
            } else {
                $("#myModalFullFormaPagamentoContent").html(data);
            }
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('danger', 'Recebedoria', MsgNaoFoiPossivelCompletarOperacao, 'myModalFullFormaPagamentoContent');
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function RemoverFormaPagamento(objButton, id) {
    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/RemoverFormaPagamento',
        data: { Guid: id },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg, 'myModalFullFormaPagamentoContent');
            } else {
                $("#myModalFullFormaPagamentoContent").html(data);
            }
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('danger', 'Recebedoria', MsgNaoFoiPossivelCompletarOperacao, 'myModalFullFormaPagamentoContent');
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function LeiAmraFormaPagamento() {
    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/LeiAmraFormaPagamento',
        data: { LeiAmra: $("#cbTrocoLeiAmra").is(":checked") },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg, 'myModalFullFormaPagamentoContent');
            } 
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('danger', 'Recebedoria', MsgNaoFoiPossivelCompletarOperacao, 'myModalFullFormaPagamentoContent');
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function RealizarPagamento() {
    var VendaId = 0;

    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'Recebedoria/Recebimento/RealizarPagamento',
        beforeSend: function () {
            OnBeginForm();
            $("#btnRealizarPagamento").attr("disabled", "disabled");
        },
        success: function (data) {
            if (data.msg != null) {
                ShowNotify('warning', 'Recebedoria', data.msg, 'myModalFullFormaPagamentoContent');
                $("#btnRealizarPagamento").removeAttr("disabled");
                OnCompleteForm();
            } else {

                VendaId = data.VendaId;

                $("#pnlVendaComprador").html("");
                $("#pnlVendaItens").html("");

                if ($("#DataVenda").length > 0) {
                    $("#pnlSelecaoComprador").hide();
                }                

                $.ajax({
                    type: 'GET',
                    url: rootURL + 'Recebedoria/Recebimento/UltimosRecebimentos',
                    beforeSend: function () {
                        $("#pnlUltimasRecebedoria").html("");
                    },
                    success: function (data) {
                        $("#pnlUltimasRecebedoria").html(data);
                        dialogFullFormaPagamento.dialog('close');
                        EmitirRecibo(VendaId);
                    },
                    error: function (xhr, st, str) {

                    },
                    complete: function () {
                        OnCompleteForm();
                    }
                });               
            }
        },
        error: function (xhr, st, str) {
            $("#btnRealizarPagamento").removeAttr("disabled");
            OnCompleteForm();
            ShowNotify('danger', 'Recebedoria', MsgNaoFoiPossivelCompletarOperacao, 'myModalFullFormaPagamentoContent');
        },
        complete: function () {
            
        }
    });
}
/* FIM - PAGAMENTO */

/* INICIO - RECIBO */
function EmitirRecibo(VendaId) {
    $("#iFrameRecibo").attr("src", "about:blank");

    //dialogRecibo.dialog("open");
    //dialogRecibo.dialog("moveToTop");

    //$("#iFrameRecibo").css("width", parseInt($("#myModalRecibo").css("width")) - (40 + parseInt($("#myModalRecibo").css("padding-left")) + parseInt($("#myModalRecibo").css("padding-right"))) + "px");
    //$("#iFrameRecibo").css("height", parseInt($("#myModalRecibo").css("height")) - (160) + "px");

    $("#VendaId").val(VendaId);
    $("#frmRecibo").submit();    
}

function OrdemVendaEmitirRecibo(OrdemVendaId) {
    $("#iFrameReciboOrdemVenda").attr("src", "about:blank");

    //dialogRecibo.dialog("open");
    //dialogRecibo.dialog("moveToTop");

    //$("#iFrameRecibo").css("width", parseInt($("#myModalRecibo").css("width")) - (40 + parseInt($("#myModalRecibo").css("padding-left")) + parseInt($("#myModalRecibo").css("padding-right"))) + "px");
    //$("#iFrameRecibo").css("height", parseInt($("#myModalRecibo").css("height")) - (160) + "px");

    $("#OrdemVendaId").val(OrdemVendaId);
    $("#frmReciboOrdemVenda").submit();
}
/* FIM - RECIBO */