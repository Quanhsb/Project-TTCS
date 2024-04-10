import { Box, Button, Typography, Dialog, DialogTitle, DialogContent, IconButton } from "@mui/material";
import React, { useEffect, useMemo } from "react";
import axios from "../../setups/custom_axios";
import { DataGrid, GridEditInputCell } from "@mui/x-data-grid";
import { styled } from '@mui/material/styles';
import Tooltip, { tooltipClasses } from '@mui/material/Tooltip';
import CloseIcon from '@mui/icons-material/Close';
import SaveAsIcon from '@mui/icons-material/SaveAs';
const StyledBox = styled(Box)(({ theme }) => ({
  height: '50vh',
  width: 700,
  "& .MuiTablePagination-displayedRows, .MuiTablePagination-selectLabel": {
    "margin-top": "1em",
    "margin-bottom": "1em"
  },
  '& .MuiDataGrid-cell--editable': {
    '& .MuiInputBase-root': {
      height: '100%',
    },
  },
  '& .Mui-error': {
    backgroundColor: `rgb(126,10,15, ${theme.palette.mode === 'dark' ? 0 : 0.1})`,
    color: theme.palette.mode === 'dark' ? '#ff4343' : '#750f0f',
  },
}));
let promiseTimeout;
const StyledTooltip = styled(({ className, ...props }) => (
  <Tooltip {...props} classes={{ popper: className }} />
))(({ theme }) => ({
  [`& .${tooltipClasses.tooltip}`]: {
    backgroundColor: theme.palette.error.main,
    color: theme.palette.error.contrastText,
  },
}));
function NameEditInputCell(props) {
  const { error } = props;
  return (
    <StyledTooltip open={!!error} title={(props?.value === null || props?.value === undefined) ? "Bạn chưa điền thông tin" : "Số tiền không hợp lệ"}>
      <GridEditInputCell {...props} />
    </StyledTooltip>
  );
}
const dataPreUpdate = [];
const CreateListRevenue = ({ openModal, setOpenModal, dataHouseHold, setDataHouseHold }) => {
  function renderEditName(params) {
    const item = dataPreUpdate.find(o => o.id === params.id && o.field === params.field);
    if (!item) dataPreUpdate.push({ id: params.id, field: params.field, error: params.error });
    else item.error = params.error;
    return <NameEditInputCell {...params} />;
  }
  const preProcessEditCellProps = async (params) => {
    const hasError = params.props.value === null || params.props.value < 0;
    return { ...params.props, error: hasError };
  };
  useEffect(
    async () => {
      try {
        const response = await axios.get(`/ho-khau/danh-sach-ho-khau`);
        console.log(response);
        var list = response.data.map((value, index) => {
          return { id: index.toString(), maHoKhau: value.maHoKhau, dien: 0, nuoc: 0, internet: 0 };
        });
        setDataHouseHold(() => list);
        console.log(list);
      } catch (error) {
        console.log(error);
      }
      return
    }, []);
  const checkData = () => {
    console.log(dataPreUpdate);
    for (const item of dataPreUpdate) {
      if (item.error) return false;
    }
    return true;
  }
  const columns = useMemo(() => [
    { field: "maHoKhau", headerName: "Mã hộ khẩu", flex: 0.5 },
    {
      field: "dien",
      headerName: "Tiền điện",
      flex: 1,
      type: "number",
      preProcessEditCellProps,
      editable: true,
      renderEditCell: renderEditName,
    },
    {
      field: "nuoc",
      headerName: "Tiền nước",
      flex: 1,
      type: "number",
      preProcessEditCellProps,
      editable: true,
      renderEditCell: renderEditName,
    },
    {
      field: "internet",
      headerName: "Tiền internet",
      flex: 1,
      type: "number",
      preProcessEditCellProps,
      editable: true,
      renderEditCell: renderEditName,
    },
  ]);
  const processRowUpdate = (newRow) => {
    console.log(newRow);
    setDataHouseHold(dataHouseHold.map((item) => (item.id === newRow.id ? newRow : item)));
    return newRow;
  };
  const handleProcessRowUpdateError = (error) => {
    console.log({ children: error.message, severity: 'error' });
  }
  useEffect(() => {
    return () => {
      clearTimeout(promiseTimeout);
    };
  }, []);
  return <Dialog open={openModal} maxWidth="md" style={{ backgroundColor: "transparent" }}>
    <DialogTitle>
      <div style={{ display: 'flex' }}>
        <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
          {"Tạo khoản thu phí sinh hoạt"}
        </Typography>
        <IconButton aria-label="close" onClick={() => {
          setOpenModal(!openModal)
        }}>
          <CloseIcon />
        </IconButton>
      </div>
    </DialogTitle>
    <DialogContent dividers>
      <StyledBox>
        <DataGrid
          getRowId={(row) => row.id}
          rows={dataHouseHold}
          columns={columns}
          processRowUpdate={processRowUpdate}
          onProcessRowUpdateError={handleProcessRowUpdateError}
          experimentalFeatures={{ newEditingApi: true }}
        />
      </StyledBox>
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 20 }}>
        <Button color="secondary" variant="contained" startIcon={<SaveAsIcon />}
          onClick={() => {
            if (checkData() && window.confirm("Bạn chắc chắn muốn lưu?") === true) {
              setOpenModal(false);
            }
          }}>
          Lưu
        </Button>
      </div>
    </DialogContent>
  </Dialog>;
}
export default CreateListRevenue;