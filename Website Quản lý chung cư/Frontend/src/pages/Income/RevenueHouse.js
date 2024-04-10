import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import ManageAccountsRoundedIcon from '@mui/icons-material/ManageAccountsRounded';
import Button from '@mui/material/Button';
import { useMemo, useState } from "react";
import { Triangle } from "react-loader-spinner";
import { useSelector, useDispatch } from "react-redux";
import { setRevenueItemID, setRevenueHouseID, setRevenueItemType } from "../../Redux/revenueSlice";
import PayRevenue from "./PayRevenue";
import moment from "moment";
const RevenueHouse = () => {
    const dispatch = useDispatch();
    const [openPopup, setOpenPopup] = useState(false);
    const maKhoanThu = useSelector((state) => state.revenue.maKhoanThu);
    const revenueHouse = useSelector((state) => state.revenue.revenueHouse);
    const revenueHouseID = useSelector((state) => state.revenue.revenueHouseID);
    const isLoadingHouse = useSelector((state) => state.revenue.isLoadingHouse);
    const [soTienCanThu, setSoTienCanThu] = useState(0);
    const [maHoKhau, setMaHoKhau] = useState();
    const PayButton = ({ maKhoanThuTheoHo, openPopup, setOpenPopup, maKhoanThu, soTienCanThu, loaiKhoanThu, maHoKhau }) => {
        return (
            <Button onClick={() => {
                dispatch(setRevenueItemID(maKhoanThu));
                dispatch(setRevenueHouseID(maKhoanThuTheoHo));
                dispatch(setRevenueItemType(loaiKhoanThu));
                setMaHoKhau(maHoKhau);
                setSoTienCanThu(soTienCanThu);
                setOpenPopup(!openPopup);
            }}
                startIcon={<ManageAccountsRoundedIcon />}
                variant="contained"
                color="info"
                style={{ border: "none" }}>Thanh toán
            </Button>
        );
    }
    const columns = useMemo(() => [
        { 
            field: "maKhoanThu", 
            headerName: "Mã khoản thu", 
            flex: 0.5 
        },
        { 
            field: "maKhoanThuTheoHo", 
            headerName: "Mã khoản thu theo hộ", 
            flex: 0.7 
        },
        {
            field: "tenKhoanThu",
            headerName: "Tên khoản thu",
            flex: 1,
            cellClassName: "name-column--cell",
        },
        {
            field: "soTien",
            headerName: "Số tiền cần thu",
            flex: 1,
            valueGetter: (param) => { return ((param.row.soTien ? param.row.soTien : "0") + " đồng") },
        },
        {
            field: "soTienDaNop",
            headerName: "Số tiền đã nộp",
            flex: 1,
            valueGetter: (param) => { return (param.row.soTienDaNop + " đồng") },
        },
        {
            field: "tinhTrang",
            headerName: "Tình trạng",
            flex: 1,
            valueGetter: (param) => {
                if (param.row.soTien == null) return ("Đóng góp");
                return ((param.row.soTienDaNop >= param.row.soTien) ? "Đã nộp đủ" : ("Còn thiếu " + (param.row.soTien - param.row.soTienDaNop) + " đồng"));
            },
        },
        {
            field: "thanhToan",
            headerName: "Thanh toán",
            flex: 1,
            disableExport: true,
            renderCell: (param) => {
                return (param.row.soTienDaNop < param.row.soTien || param.row.loaiKhoanThu === 0) && <PayButton openPopup={openPopup}
                    soTienCanThu={param.row.soTien - param.row.soTienDaNop} loaiKhoanThu={param.row.loaiKhoanThu} maHoKhau={param.row.maHoKhau}
                    setOpenPopup={setOpenPopup} maKhoanThuTheoHo={param.row.maKhoanThuTheoHo} />
            },
        },
    ]);
    return (
        <Box m="20px">
            <Header
                title="Danh sách khoản thu theo hộ"
            />
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
                <PayRevenue openPopup={openPopup} setOpenPopup={setOpenPopup} maKhoanThuTheoHo={revenueHouseID} maKhoanThu={maKhoanThu} soTienCanThu={soTienCanThu} maHoKhau={maHoKhau} />
                {isLoadingHouse ? (
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
                        rows={revenueHouse}
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
                fileName: `Danh sách khoản thu theo hộ ${moment().format('YYYY-MM-DD')}`,
                utf8WithBom: true,
            }}
            />
        </GridToolbarContainer>
    );
}
export default RevenueHouse;