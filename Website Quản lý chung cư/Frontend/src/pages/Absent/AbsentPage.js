import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import ManageAccountsRoundedIcon from '@mui/icons-material/ManageAccountsRounded';
import Button from '@mui/material/Button';
import { fetchAllAbsents } from "../../Redux/absentSlice";
import { useEffect, useMemo, useState } from "react";
import { Triangle } from "react-loader-spinner";
import { useSelector, useDispatch } from "react-redux";
import EditAbsent from "./EditAbsent";
import RegisterAbsent from "./RegisterAbsent";
import absentService from "../../Services/API/absentService";
import dayjs from "dayjs";
import moment from "moment";
const AbsentPage = () => {
  const dispatch = useDispatch();
  const [openPopup, setOpenPopup] = useState(false);
  const [openInPopup, setOpenInPopup] = useState(false);
  const { absentList, isLoading } = useSelector((state) => state.absent);
  const [data, setData] = useState([]);
  const EditButton = ({ maTamVang, openInPopup, setOpenInPopup }) => {
    return (
      <Button onClick={() => {
        absentService.getAbsent(maTamVang).then(mes => {
          setData(mes.data);
          setOpenInPopup(!openInPopup);
        })
      }}
        startIcon={<ManageAccountsRoundedIcon />}
        variant="contained"
        color="info">Chi tiết
      </Button>
    );
  }
  useEffect(() => {
    if (!isLoading) {
      dispatch(fetchAllAbsents());
    }
  }, []);
  const columns = useMemo(() => [
    { field: "maTamVang", headerName: "Mã tạm vắng", flex: 0.5 },
    {
      field: "hoTen",
      headerName: "Họ tên",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "lyDo",
      headerName: "Lý do",
      flex: 1,
    },
    {
      field: "canCuocCongDan",
      headerName: "Căn cước công dân",
      flex: 1,
    },
    {
      field: "thoiHan",
      headerName: "Ngày bắt đầu",
      flex: 1,
      valueGetter: (param) => { return dayjs(param.row.thoiHan).format('DD/MM/YYYY') },
    },
    {
      field: "chiTiet",
      headerName: "",
      flex: 1,
      disableExport: true,
      renderCell: (param) => <EditButton maTamVang={param.row.maTamVang} openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} />,
    }
  ]);
  return (
    <Box m="20px">
      <Header
        title="Danh sách tạm vắng"
      />
      <Button onClick={() => {
        setOpenPopup(!openPopup);
      }}
        color="info" variant="contained" style={{ fontWeight: "bold" }}>
        Đăng ký tạm vắng</Button>
      <Box
        m="40px 0 0 0"
        height="75vh"
        sx={{
          "& .MuiTablePagination-displayedRows, .MuiTablePagination-selectLabel": {
            "margin-top": "1em",
            "margin-bottom": "1em"
          }
        }}
      >
        <RegisterAbsent openPopup={openPopup} setOpenPopup={setOpenPopup}>
        </RegisterAbsent>
        <EditAbsent openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} data={data}>
        </EditAbsent>
        {isLoading ? (
          <div className="loading-container d-flex flex-column align-items-center ustify-content-center">
            <Triangle
              height="100"
              width="100"
              color="#1877f2"
              ariaLabel="loading"
            />
            <div>Loading data...</div>
          </div>
        ) : (
          <DataGrid
            getRowId={(r) => r.maTamVang}
            rows={absentList}
            columns={columns}
            components={{ Toolbar: CustomToolbar }}
          />
        )}
      </Box>
    </Box>
  );
};
function CustomToolbar() {
  return (
    <GridToolbarContainer>
      <GridToolbarColumnsButton />
      <GridToolbarFilterButton />
      <GridToolbarDensitySelector />
      <GridToolbarExport csvOptions={{
        fileName: `Danh sách tạm vắng ${moment().format('YYYY-MM-DD')}`,
        utf8WithBom: true,
      }}
      />
    </GridToolbarContainer>
  );
}
export default AbsentPage;