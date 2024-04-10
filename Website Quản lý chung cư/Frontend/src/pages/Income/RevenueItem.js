import { Box } from "@mui/material";
import { DataGrid, GridToolbarColumnsButton, GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport, GridToolbarFilterButton } from "@mui/x-data-grid";
import Header from "../../components/Header";
import ManageAccountsRoundedIcon from '@mui/icons-material/ManageAccountsRounded';
import Button from '@mui/material/Button';
import { useEffect, useMemo, useState } from "react";
import { Triangle } from "react-loader-spinner";
import { useSelector } from "react-redux";
import PayRevenue from "./PayRevenue";
import { useHistory } from "react-router-dom";
import moment from "moment";
const RevenueItem = () => {
    const history = useHistory();
    const revenueItem = useSelector((state) => state.revenue.revenueItem)
    const isLoadingItem = useSelector((state) => state.revenue.isLoadingItem);
    const idKhoanThu = useSelector((state) => state.revenue.maKhoanThu);
    const loaiKhoanThu = useSelector((state) => state.revenue.loaiKhoanThu);
    const [id, setId] = useState(0);
    const [openPopup, setOpenPopup] = useState(false);
    const [soTienCanThu, setSoTienCanThu] = useState(0);
    const PayButton = ({ maKhoanThuTheoHo, openPopup, setOpenPopup, soTienCanThu }) => {
        return (
            <Button onClick={() => {
                setId(maKhoanThuTheoHo);
                setSoTienCanThu(soTienCanThu);
                setOpenPopup(!openPopup);
            }}
                startIcon={<ManageAccountsRoundedIcon />}
                variant="contained"
                color="info">Thanh toán
            </Button>
        );
    }
    const columns = useMemo(() => [
        { 
            field: "maKhoanThuTheoHo", 
            headerName: "Mã khoản thu theo hộ", 
            flex: 0.7 
        },
        {
            field: "maHoKhau",
            headerName: "Mã hộ khẩu",
            flex: 0.5,
            cellClassName: "name-column--cell",
        },
        {
            field: "soTien",
            headerName: "Số tiền cần thu",
            flex: 1,
            valueGetter: (param) => { return (param.row.soTien + " đồng") },
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
                if (loaiKhoanThu === 0) return ("Đóng góp");
                return ((param.row.soTienDaNop >= param.row.soTien) ? "Đã nộp đủ" : ("Còn thiếu " + (param.row.soTien - param.row.soTienDaNop) + " đồng"));
            },
        },
        {
            field: "thanhToan",
            headerName: "Thanh toán",
            flex: 1,
            disableExport: true,
            renderCell: (param) => {
                return (param.row.soTienDaNop < param.row.soTien || loaiKhoanThu === 0) && <PayButton openPopup={openPopup}
                    soTienCanThu={param.row.soTien - param.row.soTienDaNop}
                    setOpenPopup={setOpenPopup} maKhoanThuTheoHo={param.row.maKhoanThuTheoHo} />
            },
        },
    ]);
    useEffect(() => {
        if (!idKhoanThu) {
            history.push('/revenue');
        }
    }, [idKhoanThu]);
    return (
        <Box m="20px">
            <Header
                title="Danh sách các hộ cần thu phí"
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
                <PayRevenue openPopup={openPopup} setOpenPopup={setOpenPopup} maKhoanThuTheoHo={id} maKhoanThu={idKhoanThu} soTienCanThu={soTienCanThu} />
                {isLoadingItem ? (
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
                        getRowId={(r) => r.maKhoanThuTheoHo}
                        rows={revenueItem}
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
                fileName: `Danh sách các hộ cần thu phí ${moment().format('YYYY-MM-DD')}`,
                utf8WithBom: true,
            }}
            />
        </GridToolbarContainer>
    );
}
export default RevenueItem;