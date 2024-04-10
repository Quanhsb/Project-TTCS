import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import { Triangle } from "react-loader-spinner";
import React, { useEffect, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import dayjs from "dayjs";
import ManageAccountsRoundedIcon from '@mui/icons-material/ManageAccountsRounded';
import Button from '@mui/material/Button';
import { fetchAllDemographic } from "../../Redux/demographicSlice";
import demographicService from "../../Services/API/demographicService";
import RegisterDemographic from "./RegisterDemographic";
import EditDemographic from "./EditDemographic";
import moment from "moment";
const DemographicPage = () => {
  const dispatch = useDispatch();
  const [openPopup, setOpenPopup] = useState(false);
  const [openInPopup, setOpenInPopup] = useState(false);
  const { demographicList, isLoading } = useSelector((state) => state.demographic);
  const [data, setData] = useState([]);
  const EditButton = ({ maNhanKhau, openInPopup, setOpenInPopup }) => {
    return (
      <Button onClick={() => {
        demographicService.getDemographic(maNhanKhau).then(mes => {
          setData(mes.data);
          setOpenInPopup(!openInPopup);
        });
      }}
        startIcon={<ManageAccountsRoundedIcon />}
        variant="contained"
        color="info">
        Chi tiết
      </Button>
    );
  }
  useEffect(() => {
    if (!isLoading) {
      dispatch(fetchAllDemographic());
    }
  }, []);
  useEffect(() => {
  }, [demographicList, isLoading]);
  const columns = useMemo(() => [
    {
      field: "maNhanKhau",
      headerName: "Mã nhân khẩu",
      flex: 0.5,
    },
    {
      field: "hoTen",
      headerName: "Họ tên",
      flex: 0.75,
    },
    {
      field: "canCuocCongDan",
      headerName: "Căn cước công dân",
      type: 'string',
      flex: 0.75,
    },
    {
      field: "ngaySinh",
      headerName: "Ngày sinh",
      flex: 0.5,
      valueGetter: (param) => { return dayjs(param.row.ngaySinh).format('DD/MM/YYYY') },
    },
    {
      field: "danToc",
      headerName: "Dân tộc",
      flex: 0.5,
    },
    {
      field: "trangThai",
      headerName: "Trạng thái",
      flex: 0.5,
      valueGetter: (param) => { return ((param.row.trangThai === 1) ? "Còn sống" : "Đã mất") },
    },
    {
      field: "chiTiet",
      headerName: "",
      flex: 1,
      disableExport: true,
      renderCell: (param) => <EditButton maNhanKhau={param.row.maNhanKhau} openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} />,
    }
  ]);
  return (
    <Box m="20px">
      <Header
        title="Danh sách nhân khẩu"
      />
      <Button onClick={() => {
        setOpenPopup(!openPopup);
      }}
        color="info" variant="contained" style={{ fontWeight: "bold" }}>
        Đăng ký nhân khẩu</Button>
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
        <RegisterDemographic openPopup={openPopup} setOpenPopup={setOpenPopup} />
        <EditDemographic openInPopup={openInPopup} setOpenInPopup={setOpenInPopup} data={data} />
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
            getRowId={(r) => r.maNhanKhau}
            rows={demographicList}
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
        fileName: `Danh sách nhân khẩu ${moment().format('YYYY-MM-DD')}`,
        utf8WithBom: true,
      }}
      />
    </GridToolbarContainer>
  );
}
export default DemographicPage;