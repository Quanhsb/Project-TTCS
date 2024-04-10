import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import ManageAccountsRoundedIcon from '@mui/icons-material/ManageAccountsRounded';
import Button from '@mui/material/Button';
import { fetchAllTabernacles } from "../../Redux/tabernacleSlice";
import { useEffect, useMemo, useState } from "react";
import { Triangle } from "react-loader-spinner";
import { useSelector, useDispatch } from "react-redux";
import RegisterTabernacle from "./RegisterTabernacle";
import EditTabernacle from "./EditTabernacle";
import moment from "moment";
const TabernaclePage = () => {
  const dispatch = useDispatch();
  const [openPopup, setOpenPopup] = useState(false);
  const [openInPopup, setOpenInPopup] = useState(false);
  const { tabernacleList, isLoading } = useSelector((state) => state.tabernacle);
  const [data, setData] = useState([]);
  const EditButton = ({ maTamTru, openInPopup, setOpenInPopup }) => {
    return (
      <Button onClick={() => {
        tabernacleService.getTabernacle(maTamTru).then(mes => {
          setData(mes.data);
          setOpenInPopup(!openInPopup);
        });
      }}
        startIcon={<ManageAccountsRoundedIcon />}
        variant="contained"
        color="info">Chi tiết
      </Button>
    );
  }
  useEffect(() => {
    if (!isLoading) {
      dispatch(fetchAllTabernacles());
    }
  }, []);
  useEffect(() => {
  }, [tabernacleList]);
  const columns = useMemo(() => [
    { field: "maTamTru", headerName: "Mã tạm trú", flex: 0.5 },
    {
      field: "hoTen",
      headerName: "Họ tên",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "diaChiThuongTru",
      headerName: "Địa chỉ thường trú",
      flex: 1,
    },
    {
      field: "diaChiTamTru",
      headerName: "Địa chỉ tạm trú",
      flex: 1,
    },
    {
      field: "canCuocCongDan",
      headerName: "Căn cước công dân",
      flex: 1,
    },
    {
      field: "chiTiet",
      headerName: "",
      flex: 1,
      disableExport: true,
      renderCell: (param) => <EditButton maTamTru={param.row.maTamTru} openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} />,
    }
  ]);
  return (
    <Box m="20px">
      <Header
        title="Danh sách tạm trú"
      />
      <Button onClick={() => {
        setOpenPopup(!openPopup);
      }}
        color="info" variant="contained" style={{ fontWeight: "bold" }}>
        Đăng ký tạm trú</Button>
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
        <RegisterTabernacle openPopup={openPopup} setOpenPopup={setOpenPopup}>
        </RegisterTabernacle>
        <EditTabernacle openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} data={data}>
        </EditTabernacle>
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
            getRowId={(r) => r.maTamTru}
            rows={tabernacleList}
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
        fileName: `Danh sách tạm trú ${moment().format('YYYY-MM-DD')}`,
        utf8WithBom: true,
      }}
      />
    </GridToolbarContainer>
  );
}
export default TabernaclePage;