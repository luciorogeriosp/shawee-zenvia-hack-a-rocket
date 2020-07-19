/* INÍCIO - FILTROS */
function AbrirResultadoBuscaConsultaMembro(Id, TipoId, fSeqCadast, Selecionar) {
    ClearNotify();

    var plus = false;

    $('[id*="pnlResultadoBuscaConsultaMembro_VisaoGeral_"]').each(function () {
        $(this).hide();
    });

    if ($("#iconResultadoBuscaConsultaMembro_" + Id).hasClass("fa-plus")) {
        plus = true;
        $('[id*="pnlResultadoBuscaConsultaMembro_VisaoGeral_' + Id + '"]').show();
    }

    $('[id*="iconResultadoBuscaConsultaMembro_"]').each(function () {
        $(this).removeClass("fa-minus");
        $(this).removeClass("fa-plus");

        $(this).addClass("fa-plus");
    });

    if (plus) {
        $("#iconResultadoBuscaConsultaMembro_" + Id).removeClass("fa-plus");
        $("#iconResultadoBuscaConsultaMembro_" + Id).addClass("fa-minus");
    }
}

function FiltroMembroPorLetra(obj, letra) {
    $(obj).parent().parent().find("li").removeClass("active");
    $(obj).parent().addClass("active");

    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/ListaPorLetra',
        data: { Letra: letra },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            $("#ListaMembro").html(data);
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

function ExecutaFiltroMembroBusca(ativo) {
    ClearNotify();
    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/ListaPorBusca',
        data: { Ativo: ativo },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            $("#ListaMembro").html(data);

            if (ativo == undefined) {
                $("#btnFitroDesligado").removeAttr("disabled");
                $("#btnFitroAtivoInativo").removeAttr("disabled");
                $("#btnFitroTodos").attr("disabled", "disabled");
            } else {
                if (ativo) {
                    $("#btnFitroTodos").removeAttr("disabled");
                    $("#btnFitroDesligado").removeAttr("disabled");
                    $("#btnFitroAtivoInativo").attr("disabled", "disabled");
                } else {
                    $("#btnFitroTodos").removeAttr("disabled");
                    $("#btnFitroAtivoInativo").removeAttr("disabled");
                    $("#btnFitroDesligado").attr("disabled", "disabled");

                }
            }
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

function ExecutaExportacaoMembroBusca(ativo) {
    ClearNotify();

    $("#iFrameReciboAutorizacaoTemplo").prop("src", rootURL + 'OrganismoAfiliado/Membro/ExportarPorBusca?ativo=' + ativo);
}
/* FIM - FILTROS */

/* INÍCIO - DETALHES */
function AbrirDetalhesMembro(Id) {
    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/Detalhe',
        data: { Id: Id },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            $("#myModalFullContent").html(data);
            OpenFullDialog();
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function AbrirDetalhesMembroDual(Id) {
    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/Detalhe',
        data: { Id: Id },
        beforeSend: function () {
            OnBeginForm();
        },
        success: function (data) {
            $("#myModalFullContent").html(data);
            OpenFullDialog();
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            OnCompleteForm();
        }
    });
}

function AbrirDetalhesMembroFinanceiro(ModelMembro) {
    $("#SituacaoFinanceira").html("");
    ShowProgress("SituacaoFinanceira");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheFinanceiro',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#SituacaoFinanceira").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroIniciacoes(ModelMembro) {
    $("#Iniciacoes").html("");
    ShowProgress("Iniciacoes");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheIniciacoes',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#Iniciacoes").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroHistoricoCompras(ModelMembro) {
    $("#HistoricoCompras").html("");
    $("#HistoricoCompras").html("");

    ShowProgress("HistoricoCompras");
    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheHistoricoCompras',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#HistoricoCompras").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroHistoricoContribuicoes(ModelMembro) {
    $("#HistoricoContribuicoes").html("");
    ShowProgress("HistoricoContribuicoes");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheHistoricoContribuicoes',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#HistoricoContribuicoes").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroHistoricoAtividades(ModelMembro) {
    $("#HistoricoAtividades").html("");
    ShowProgress("HistoricoAtividades");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheHistoricoAtividades',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#HistoricoAtividades").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroHistoricoBiblioteca(ModelMembro) {
    $("#HistoricoBiblioteca").html("");
    ShowProgress("HistoricoBiblioteca");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/DetalheHistoricoBiblioteca',
        data: ModelMembro,
        beforeSend: function () {

        },
        success: function (data) {
            $("#HistoricoBiblioteca").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AbrirDetalhesMembroHistoricoComprasDetalhado(VendaId) {

    if ($("#pnlHistoricoCompras_Itens_" + VendaId).length == 0) {
        var strHtml = "<tr><td>&nbsp;</td><td colspan=\"9\" style=\"padding-bottom:15px; padding-top:15px\"><div id=\"pnlHistoricoCompras_Itens_" + VendaId + "\"></div></td></tr>";
        $(strHtml).insertAfter("#pnlHistoricoCompras_" + VendaId);
        ShowProgress("pnlHistoricoCompras_Itens_" + VendaId);

        $.ajax({
            type: 'POST',
            url: rootURL + 'OrganismoAfiliado/Membro/DetalheHistoricoComprasDetalhado',
            data: { VendaId: VendaId },
            beforeSend: function () {

            },
            success: function (data) {
                $("#pnlHistoricoCompras_Itens_" + VendaId).html(data);

                $("#pnlHistoricoCompras_" + VendaId + " td a icon").removeClass("fa-plus");
                $("#pnlHistoricoCompras_" + VendaId + " td a icon").addClass("fa-minus");
            },
            error: function (xhr, st, str) {
                ShowError(MsgNaoFoiPossivelCompletarOperacao);
            },
            complete: function () {
            }
        });
    } else {
        if ($("#pnlHistoricoCompras_" + VendaId + " td a icon").hasClass("fa-plus")) {
            $("#pnlHistoricoCompras_" + VendaId + " td a icon").removeClass("fa-plus");
            $("#pnlHistoricoCompras_" + VendaId + " td a icon").addClass("fa-minus");
            $("#pnlHistoricoCompras_Itens_" + VendaId).parent().parent().show();
        } else {
            $("#pnlHistoricoCompras_" + VendaId + " td a icon").removeClass("fa-minus");
            $("#pnlHistoricoCompras_" + VendaId + " td a icon").addClass("fa-plus");
            $("#pnlHistoricoCompras_Itens_" + VendaId).parent().parent().hide();
        }


    }
}
/* FIM - FILTROS */

/* INÍNIO - NOVO MEMBRO */
var dialogNovoMembro = null;

function ConfiguraModalNovoMembro() {
    dialogNovoMembro = $("#myModalNovoMembro").dialog({
        autoOpen: false,
        width: (thisDevice == "mobile") ? parseInt($(window).width()) : parseInt($("#content").css("width")) / 2,
        resizable: false,
        modal: true,
        closeOnEscape: false,
        position: { my: "center top", at: "center top", of: $("#content") }
    });
}

function AbrirModalNovoMembro() {
    $("#myModalNovoMembroContent").html("");

    $.ajax({
        type: 'GET',
        url: rootURL + 'OrganismoAfiliado/Membro/NovoMembro',
        beforeSend: function () {
            OpenDialogNovoMembro();
        },
        success: function (data) {
            $("#myModalNovoMembroContent").html(data);
            $("#btnConfirmarNovoMembro").show();
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            $(document.forms['frmNovoMembro'].ddlNomeMembro).attr("disabled", "disabled");
            $(document.forms['frmNovoMembro'].DataInscricao).attr("disabled", "disabled");
            $(document.forms['frmNovoMembro'].DataAdmissao).attr("disabled", "disabled");

            if ($("#frmNovoMembro").length > 0) {
                if ($("#OrganismoAfiliadoId option").length == 1) {
                    document.forms['frmNovoMembro'].NumeroAfiliacao.focus();
                }
                else {
                    document.forms['frmNovoMembro'].OrganismoAfiliadoId.focus();
                }
            }
        }
    });
}

function CloseDialogNovoMembro() {
    dialogNovoMembro.dialog("close");
}

function OpenDialogNovoMembro() {
    dialogNovoMembro.dialog("open");
    dialogNovoMembro.dialog("moveToTop");
}

function DesabilitaNovoMembro() {
    $(".ui-dialog-titlebar-close").attr("disabled", "disabled");
    $("#myModalNovoMembro .modal-footer button").attr("disabled", "disabled");
    $("#myModalNovoMembro input, #myModalNovoMembro select").attr("disabled", "disabled");
}

function HabilitaNovoMembro() {
    $(".ui-dialog-titlebar-close").removeAttr("disabled");
    $("#myModalNovoMembro .modal-footer button").removeAttr("disabled");
    $("#myModalNovoMembro input, #myModalNovoMembro select").removeAttr("disabled");
}

function ConfirmaNovoMembro(obj) {
    $("#btnConfirmarNovoMembro").attr("disabled", "disabled");

    ClearNotify();

    $(document.forms['frmNovoMembro'].PrimeiroNome).value = $(document.forms['frmNovoMembro'].ddlNomeMembro).text();

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/NovoMembro',
        data: $.param($("#frmNovoMembro").serializeArray()),
        beforeSend: function () {
            DesabilitaNovoMembro();
            ShowProgress("myModalNovoMembroContent");
        },
        success: function (data) {
            $("#myModalNovoMembroContent").html(data);
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            HabilitaNovoMembro();
            OnCompleteForm();
        }
    });
}

function BuscarNovoMembro(obj) {
    ClearNotify();

    $("#ddlNomeMembro").empty();
    $("#ddlNomeMembro").attr("disabled", "disabled");
    $("#btnConfirmarNovoMembro").attr("disabled", "disabled");
    $("#DataInscricao").attr("disabled", "disabled");
    $("#DataAdmissao").attr("disabled", "disabled");

    consultaMembroSelecionarFuncao = "SelecionarResultadoBuscarNovoMembro";
    OpenDialogAbrirMembroGLP(true);
}

function SelecionarResultadoBuscarNovoMembro(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast) {

    document.forms['frmNovoMembro'].NumeroAfiliacao.value = Chave;
    document.forms['frmNovoMembro'].NumeroAfiliacaoDisabled.value = Chave;
    var NumeroAfiliacao = document.forms['frmNovoMembro'].NumeroAfiliacao.value;

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Recepcao/BuscarMembro',
        data: { fSeqCadast: fSeqCadast },
        beforeSend: function () {
            CloseDialogAbrirMembroGLP();
            OnBeginForm();
        },
        success: function (data) {
            if (data.success) {
                OnCompleteForm();

                RecepcaoMembroModel = JSON.parse(data.model);

                if (RecepcaoMembroModel.ListaMembros === null) {
                    ShowNotify('warning', 'Organismo Afiliado | Membros', 'Não foi possível localizar nenhum membro com número de afiliação informado.', 'myModalNovoMembroContent');
                    OnCompleteForm();
                } else {
                    $("#ddlNomeMembro").removeAttr("disabled");

                    if (RecepcaoMembroModel.ListaMembros.length > 1) {
                        $("#ddlNomeMembro").append("<option value=''>MEMBRO DUAL - Selecione");
                    } else {
                        if (RecepcaoMembroModel.ListaMembros[0].MembroOGG == true) {
                            ShowNotify('warning', 'Organismo Afiliado | Membros', 'No momento não é permitida a inclusão de membros da OGG.', 'myModalNovoMembroContent');
                            OnCompleteForm();
                            return;
                        }
                    }

                    $.each(RecepcaoMembroModel.ListaMembros, function (index) {
                        if (this.MembroOGG == false) {
                            $("#ddlNomeMembro").append("<option value='" + this.fSeqCadast + "'>" + this.Nome.toUpperCase());
                        }
                    });

                    if (RecepcaoMembroModel.ListaMembros.length == 1) {
                        SelecionaNovoMembro();
                    }
                }
            } else {
                OnCompleteForm();
                ShowNotify('warning', 'Organismo Afiliado | Membros', data.msg, 'myModalNovoMembroContent');
            }
        },
        error: function (xhr, st, str) {
            OnCompleteForm();
            ShowNotify('warning', 'Organismo Afiliado | Membros', MsgNaoFoiPossivelCompletarOperacao, 'myModalNovoMembroContent');
        },
        complete: function () {

        }
    });
}

function SelecionaNovoMembro() {
    if ($("#ddlNomeMembro").val() != "" && $("#OrganismoAfiliadoId").val() != "") {
        $("#btnConfirmarNovoMembro").removeAttr("disabled");
        $("#DataInscricao").removeAttr("disabled");
        $("#DataAdmissao").removeAttr("disabled");

        document.forms['frmNovoMembro'].DataInscricao.focus();
    } else {
        $("#btnConfirmarNovoMembro").attr("disabled", "disabled");
        $("#DataInscricao").attr("disabled", "disabled");
        $("#DataAdmissao").attr("disabled", "disabled");

        document.forms['frmNovoMembro'].NumeroAfiliacao.focus();
    }
}
/* FIM - NOVO MEMBRO */

/* INICIO - EDIÇÃO DE DETALHES */
function EditarDetalheMembro(campo) {
    $("#" + campo).removeAttr("disabled");

    $("#btnEditarDetalheMembro" + campo).hide();
    $("#btnCancelarDetalheMembro" + campo).show();
    $("#btnGravarDetalheMembro" + campo).show();

    $("#" + campo).focus();
}

function CancelarDetalheMembro(campo) {
    $("#" + campo).attr("disabled", "disabled");
    $("#DadosCadastrais .alert").remove();

    $("#btnEditarDetalheMembro" + campo).show();
    $("#btnCancelarDetalheMembro" + campo).hide();
    $("#btnGravarDetalheMembro" + campo).hide();

    $("#" + campo).val($("#" + campo + "_hidden").val());
}

function GravarDetalheMembroEnter(event, campo, ModelMembro) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        GravarDetalheMembro(campo, ModelMembro);
    }
}

function GravarDetalheMembro(campo, ModelMembro) {

    var div = "";
    if ($("#pnl" + campo).length > 0) {
        div = "pnl" + campo;
    }

    $("#" + campo).attr("disabled", "disabled");

    $("#DadosCadastrais .alert").remove();

    $("#btnEditarDetalheMembro" + campo).hide();
    $("#btnCancelarDetalheMembro" + campo).hide();
    $("#btnGravarDetalheMembro" + campo).hide();

    $("#pnlLoadingDetalheMembro" + campo).show();

    ClearNotify();

    if ($("#" + campo + "_hidden").val() != $("#" + campo).val()) {

        var extra = "";
        if ($("#" + campo + "_extra_hidden").length > 0) {
            extra = $("#" + campo + "_extra_hidden").val();
        }

        $.ajax({
            type: 'POST',
            url: rootURL + 'OrganismoAfiliado/Membro/DetalheGravarDetalhe',
            data: {
                MembroId: ModelMembro.Id,
                Campo: campo,
                Valor: $("#" + campo).val(),
                Extra: extra
            },
            beforeSend: function () {

            },
            success: function (data) {
                if (data.success) {
                    $("#btnEditarDetalheMembro" + campo).show();
                    $("#" + campo + "_hidden").val($("#" + campo).val());
                    if (div != '') {
                        ShowNotify('success', 'Alterar Dados', data.msg, div);
                    }
                } else {
                    $("#" + campo).removeAttr("disabled");
                    $("#" + campo).val("");

                    $("#btnEditarDetalheMembro" + campo).hide();
                    $("#btnCancelarDetalheMembro" + campo).show();
                    $("#btnGravarDetalheMembro" + campo).show();

                    if (div != '') {
                        ShowNotify('warning', 'Alterar Dados', data.msg, div);
                    }

                    $("#" + campo).focus();
                }
            },
            error: function (xhr, st, str) {
                ShowError(MsgNaoFoiPossivelCompletarOperacao);
            },
            complete: function () {
                $("#pnlLoadingDetalheMembro" + campo).hide();
            }
        });
    } else {
        $("#pnlLoadingDetalheMembro" + campo).hide();
        $("#btnEditarDetalheMembro" + campo).show();
    }

    //console.log(tempModel);

}
/* FIM - EDIÇÃO DE DETALHES */

/* INICIO - TRANSFERÊNCIA DE MEMBROS */
function OpenTransferirMembro() {
    OrganismoAfiliadoId = $("#OrganismoAfiliadoId").val();

    $("#pnlTransferirMembroInicioMensagem").html("");
    $("#pnlTransferirMembroButtonsMensagem").html("");
    ClearNotify();

    ShowProgress('pnlTransferirMembroInicio');

    $("#ddlOrganismoAfiliado").empty();
    $("#ddlOrganismoAfiliado").append("<option value='-1'>Selecione");
    $("#txtOrganismoAfiliado").val("");

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/ListaOrganismosAfiliados',
        data: { OrganismoAfiliadoId: OrganismoAfiliadoId },
        beforeSend: function () {

        },
        success: function (data) {
            if (data.success) {
                $("#pnlTransferirMembroInicio").hide();
                $("#pnlTransferirMembroComboBox").show();
                $("#pnlTransferirMembroTextBox").hide();
                $("#pnlTransferirMembroButtons").show();

                $.each(data.lista, function () {
                    $("#ddlOrganismoAfiliado").append("<option value='" + this.Id + "'>" + this.Text);
                });

                $("#ddlOrganismoAfiliado").append("<option value='0'>Outro");

                $("#ddlOrganismoAfiliado").focus();
            } else {
                ShowNotify('danger', 'Organismo Afiliado - Membros', data.msg, 'pnlTransferirMembroInicioMensagem');
            }
        },
        error: function (xhr, st, str) {
            ShowNotify('danger', 'Organismo Afiliado - Membros', MsgNaoFoiPossivelCompletarOperacao, 'pnlTransferirMembroInicioMensagem');
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
            HideProgress();
        }
    });
}

function CloseTransferirMembro() {
    $("#pnlTransferirMembroInicioMensagem").html("");
    $("#pnlTransferirMembroButtonsMensagem").html("");
    ClearNotify();

    $("#pnlTransferirMembroInicio").show();
    $("#pnlTransferirMembroComboBox").hide();
    $("#pnlTransferirMembroTextBox").hide();
    $("#pnlTransferirMembroButtons").hide();
}

function TransferirMembroOutro() {

    var OrganismoAfiliadoId = $("#ddlOrganismoAfiliado").val();
    if (OrganismoAfiliadoId == "0") {
        $("#pnlTransferirMembroComboBox").hide();
        $("#pnlTransferirMembroTextBox").show();

        $("#txtOrganismoAfiliado").focus();
    }
}

function ConfirmarTransferirMembro() {
    $("#pnlTransferirMembroInicioMensagem").html("");
    $("#pnlTransferirMembroButtonsMensagem").html("");
    ClearNotify();

    var OrganismoAfiliadoId = $("#ddlOrganismoAfiliado").val();
    var Texto = "";

    if (OrganismoAfiliadoId == "-1") {
        ShowNotify('danger', 'Organismo Afiliado - Membros', 'Selecione o Organismo Afiliado para o qual irá transferir o membro.', 'pnlTransferirMembroButtonsMensagem');
    }
    else {
        if (OrganismoAfiliadoId == "0") {
            Texto = $("#txtOrganismoAfiliado").val().trim();
            if (Texto == "") {
                ShowNotify('danger', 'Organismo Afiliado - Membros', 'Informe para qual Organismo Afiliado o membro será transferido.', 'pnlTransferirMembroButtonsMensagem');
            }
        }
    }

    if (OrganismoAfiliadoId > -1 || Texto != "") {
        $.ajax({
            type: 'POST',
            url: rootURL + 'OrganismoAfiliado/Membro/Transferir',
            data: {
                OrganismoAfiliadoId: OrganismoAfiliadoId,
                MembroId: $("#MembroId").val(),
                Observacao: $("#txtOrganismoAfiliado").val(),
            },
            beforeSend: function () {
                ShowProgress('pnlTransferirMembroButtonsMensagem');
                $("#btnTransferirMembroCancelar").attr("disabled", "disabled");
                $("#btnTransferirMembroConfirmar").attr("disabled", "disabled");
            },
            success: function (data) {
                if (data.success) {
                    if (!data.transferir) {
                        $("#pnlTransferirMembroInicio").hide();
                    } else {
                        $("#pnlTransferirMembroInicio").show();
                    }

                    $("#pnlTransferirMembroComboBox").hide();
                    $("#pnlTransferirMembroTextBox").hide();
                    $("#pnlTransferirMembroButtons").hide();

                    $("#OrganismoAfiliadoNome").val(data.nome);
                    $("#OrganismoAfiliadoId").val(OrganismoAfiliadoId);
                    $("#DataInscricaoOA").val(data.inscricao);
                    $("#DataAdmissaoOA").val(data.admissao);
                    $("#ObservacaoTransferencia").val(data.observacao);

                    ShowNotify('success', 'Organismo Afiliado - Membros', 'Membro transferido com sucesso!!!', 'DadosCadastrais');
                } else {
                    ShowNotify('danger', 'Organismo Afiliado - Membros', data.msg, 'pnlTransferirMembroInicioMensagem');
                }
            },
            error: function (xhr, st, str) {
                ShowNotify('danger', 'Organismo Afiliado - Membros', MsgNaoFoiPossivelCompletarOperacao, 'pnlTransferirMembroInicioMensagem');
                ShowError(MsgNaoFoiPossivelCompletarOperacao);
            },
            complete: function () {
                HideProgress();

                $("#btnTransferirMembroCancelar").removeAttr("disabled");
                $("#btnTransferirMembroConfirmar").removeAttr("disabled");
            }
        });
    }

}
/* TERMINO - TRANSFERÊNCIA DE MEMBROS */

/* INICIO - ATALHOS */
function Apagar(Id, Nome) {
    ClearNotify();
    OpenDialogExcluir("Deseja realmente excluir o Membro " + Nome + "?", "ApagarConfirmar(" + Id + ", '" + Nome + "');")
}

function ApagarConfirmar(Id, Nome) {
    ClearNotify();

    $.ajax({
        type: 'POST',
        url: rootURL + 'OrganismoAfiliado/Membro/ApagarMembro',
        data: { Id: Id },
        beforeSend: function () {

        },
        success: function (data) {
            if (data.success) {
                ShowNotify('success', 'Organismo Afiliado - Membros', 'Membro apagado com sucesso.');
                $('#line_' + Id).remove()
            } else {
                ShowNotify('danger', 'Organismo Afiliado - Membros', data.msg);
            }
        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AtalhoIncluirMembro(nome, numero) {
    $.ajax({
        type: 'GET',
        url: rootURL + 'OrganismoAfiliado/Membro/NovoMembro',
        data: { nome: nome, numero: numero },
        beforeSend: function () {

        },
        success: function (data) {
            CloseDialogAbrirMembroGLP();
            OpenDialogNovoMembro();
            $("#myModalNovoMembroContent").html(data);
            $("#btnConfirmarNovoMembro").show();

        },
        error: function (xhr, st, str) {
            ShowError(MsgNaoFoiPossivelCompletarOperacao);
        },
        complete: function () {
        }
    });
}

function AtalhoVenda(nome, numero, oa, tom, dia) {
    ClearNotify();
    
    if (oa) {
        $.ajax({
            type: 'GET',
            data: { CodigoAfiliacao: numero, NomeMembro: nome, Completo: false },
            url: rootURL + 'OrganismoAfiliado/Membro/ConsultaMembroVisaoGeralJSON',
            beforeSend: function () {
                CloseDialogAbrirMembroGLP();
                OnBeginForm();
            },
            success: function (data) {
                if (data.success) {
                    if (data.MembroOA.substring(0, 4).indexOf('{') >= 0) {
                        objVendaMembroOA = JSON.parse(data.MembroOA);
                    } else {
                        objVendaMembroOA = JSON.parse('{' + data.MembroOA + '}');
                    }

                    $.ajax({
                        type: 'POST',
                        data: {
                            MembroOA: objVendaMembroOA
                        },
                        url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorMembro',
                        beforeSend: function () {

                        },
                        success: function (data) {
                            if (dia) {
                                if (window.location.href.substring(window.location.href.length - "Recebedoria".length) == "Recebedoria") {
                                    $("#pnlVendaComprador").html(data);
                                    HabilitaItens();
                                    OnCompleteForm();
                                } else {
                                    document.location.href = rootURL + 'Recebedoria/Recebimento';
                                }
                            } else {
                                if (window.location.href.substring(window.location.href.length - 6) == "ret=1") {
                                    $("#pnlVendaComprador").html(data);
                                    HabilitaItens();
                                    OnCompleteForm();
                                } else {
                                    document.location.href = rootURL + 'Recebedoria/Recebimento?ret=1';
                                }
                            }
                        },
                        error: function (xhr, st, str) {
                            OnCompleteForm();
                            ShowNotify('danger', 'Falha no sistema', MsgNaoFoiPossivelCompletarOperacao, 'frmConsultaMembro');
                        },
                        complete: function () {

                        }
                    });
                }

            },
            error: function (xhr, st, str) {
                OnCompleteForm();
                ShowNotify('danger', 'Falha no sistema', MsgNaoFoiPossivelCompletarOperacao, 'frmConsultaMembro');
            },
            complete: function () {

            }
        });
    } else {
        $.ajax({
            type: 'POST',
            data: {
                VisitanteNome: nome,
                VisitanteGLP: numero,
                MembroTOM: tom
            },
            url: rootURL + 'Recebedoria/Recebimento/AssistenteCompradorVisitante',
            beforeSend: function () {
                CloseDialogAbrirMembroGLP();
                OnBeginForm();
            },
            success: function (data) {
                if (data.ErrorMessage != "") {
                    ShowNotify('danger', 'Recebimento - Visitante', data.ErrorMessage, 'frmConsultaVisitante');
                    $("#frmConsultaVisitante #txtNomeVisitante").focus();
                }
                else {
                    if (dia) {
                        if (window.location.href.substring(window.location.href.length - "Recebedoria".length) == "Recebedoria") {
                            $("#pnlVendaComprador").html(data);
                            HabilitaItens();
                            OnCompleteForm();
                        } else {
                            document.location.href = rootURL + 'Recebedoria/Recebimento';
                        }
                    } else {
                        if (window.location.href.substring(window.location.href.length - 6) == "ret=1") {
                            $("#pnlVendaComprador").html(data);
                            HabilitaItens();
                            OnCompleteForm();
                        } else {
                            document.location.href = rootURL + 'Recebedoria/Recebimento?ret=1';
                        }
                    }
                }
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
}
/* TERMINO - ATALHOS */


function SelecionarResultadoBuscaConsultaMembro(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast) {
    ClearNotify();

    setTimeout(function () {
        CloseDialogAbrirMembroGLP();
    }, 1000);    

    eval(consultaMembroSelecionarFuncao + "(obj, Identificador, Chave, Nome, MembroTOM, MembroOGG, fSeqCadast)");
}