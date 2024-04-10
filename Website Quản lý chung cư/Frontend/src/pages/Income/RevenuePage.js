import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import FactCheckIcon from '@mui/icons-material/FactCheck';
import Button from '@mui/material/Button';
import { useEffect, useMemo, useState } from "react";
import { Triangle } from "react-loader-spinner";
import { useSelector, useDispatch } from "react-redux";
import { fetchAllRevenue, fetchRevenueItem, setRevenueItemID, setRevenueItemType } from "../../Redux/revenueSlice";
import dayjs from "dayjs";
import CreateRevenue from "./CreateRevenue";
import EditRevenue from "./EditRevenue";
import revenueService from "../../Services/API/revenueService";
import { Link } from "react-router-dom";
import { LIST_LOAI_KHOAN_THU } from "../../Services/Utils/const";
import EditIcon from '@mui/icons-material/Edit';
import moment from "moment";
const RevenuePage = () => {
  const dispatch = useDispatch();
  const [openPopup, setOpenPopup] = useState(false);
  const [openInPopup, setOpenInPopup] = useState(false);
  const revenueList = useSelector((state) => state.revenue.revenueList);
  const isLoadingList = useSelector((state) => state.revenue.isLoadingList);
  const [data, setData] = useState([]);
  const EditButton = ({ maKhoanThu, openInPopup, setOpenInPopup }) => {
    return (
      <Button onClick={() => {
        revenueService.getRevenue(maKhoanThu).then(mes => {
          setData(mes.data);
          setOpenInPopup(!openInPopup);
        })
      }}
        startIcon={<EditIcon />}
        variant="contained"
        color="info">Chi tiết
      </Button>
    );
  }
  const ListButton = ({ maKhoanThu, loaiKhoanThu }) => {
    return (
      <Link to="/revenue-item">
        <Button onClick={() => {
          dispatch(setRevenueItemID(maKhoanThu));
          dispatch(setRevenueItemType(loaiKhoanThu));
          dispatch(fetchRevenueItem(maKhoanThu));
        }}
          startIcon={<FactCheckIcon />}
          variant="contained"
          color="info">Danh sách hộ
        </Button>
      </Link>
    );
  }
  useEffect(() => {
    if (!isLoadingList) {
      dispatch(fetchAllRevenue());
    }
  }, []);
  useEffect(() => {
  }, [revenueList]);
  const columns = useMemo(() => [
    { field: "maKhoanThu", headerName: "Mã khoản thu", flex: 0.5 },
    {
      field: "tenKhoanThu",
      headerName: "Tên khoản thu",
      flex: 1,
      cellClassName: "name-column--cell",
    },
    {
      field: "loaiKhoanThu",
      headerName: "Loại khoản thu",
      flex: 1,
      valueGetter: (param) => {
        const khoanThu = LIST_LOAI_KHOAN_THU.find(o => o.id === param.row.loaiKhoanThu);
        return khoanThu?.label;
      },
    },
    {
      field: "thoiGianBatDau",
      headerName: "Ngày bắt đầu",
      flex: 1,
      valueGetter: (param) => { return dayjs(param.row.thoiGianBatDau).format('DD/MM/YYYY') },
    },
    {
      field: "thoiGianKetThuc",
      headerName: "Ngày kết thúc",
      flex: 1,
      valueGetter: (param) => { return dayjs(param.row.thoiGianKetThuc).format('DD/MM/YYYY') },
    },
    {
      field: "chiTiet",
      headerName: "",
      flex: 1,
      disableExport: true,
      renderCell: (param) => <EditButton maKhoanThu={param.row.maKhoanThu} openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} />,
    },
    {
      field: "danhSach",
      headerName: "",
      flex: 1,
      disableExport: true,
      renderCell: (param) => <ListButton maKhoanThu={param.row.maKhoanThu} loaiKhoanThu={param.row.loaiKhoanThu} />,
    }
  ]);
  return (
    <Box m="20px">
      <Header
        title="Danh sách khoản thu"
      />
      <Button onClick={() => {
        setOpenPopup(!openPopup);
      }}
        color="info" variant="contained" style={{ fontWeight: "bold" }}>
        Tạo khoản thu</Button>
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
        <CreateRevenue openPopup={openPopup} setOpenPopup={setOpenPopup} />
        <EditRevenue openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} data={data}></EditRevenue>
        {isLoadingList ? (
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
            getRowId={(r) => r.maKhoanThu}
            rows={revenueList}
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
        fileName: `Danh sách khoản thu ${moment().format('YYYY-MM-DD')}`,
        utf8WithBom: true,
      }}
      />
    </GridToolbarContainer>
  );
}
export default RevenuePage;