import { Box, Button, TextField, Typography, Dialog, DialogTitle, DialogContent, IconButton, MenuItem } from "@mui/material";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import SaveAsIcon from '@mui/icons-material/SaveAs';
import { useState } from "react";
import { useDispatch } from "react-redux";
import { fetchAllDemographic } from "../../Redux/demographicSlice";
import CloseIcon from '@mui/icons-material/Close';
import demographicService from "../../Services/API/demographicService";
import { DesktopDatePicker, LocalizationProvider, } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const RegisterDemographic = ({ openPopup, setOpenPopup }) => {
    const isNonMobile = useMediaQuery("(min-width:600px)");
    const dispatch = useDispatch();
    const [date, setDate] = useState(dayjs(new Date()));
    const handleFormSubmit = (values) => {
        if (window.confirm("Bạn chắc chắn muốn lưu?") === true) {
            demographicService.postDemographic({
                hoTen: values.hoTen,
                canCuocCongDan: values.canCuocCongDan,
                ngaySinh: date,
                noiSinh: values.noiSinh,
                danToc: values.danToc,
                ngheNghiep: values.ngheNghiep,
                trangThai: values.trangThai,
                quanHe: values.quanHe,
                ghiChu: values.ghiChu,
            }).then(mes => {
                toast(mes.message);
                setOpenPopup(!openPopup);
                dispatch(fetchAllDemographic());
            }).catch(e => {
                toast(e?.response?.data?.reason ?? e?.response?.data?.message ?? "Có lỗi xảy ra");
            });
        }
    };
    const initialValues = {
        hoTen: "",
        canCuocCongDan: "",
        noiSinh: "",
        danToc: "",
        ngheNghiep: "",
        trangThai: 1,
        quanHe: "",
        ghiChu: "",
    };
    return (
        <Dialog open={openPopup} maxWidth="md" style={{ backgroundColor: "transparent", overflow: 'hidden' }}>
            <DialogTitle>
                <div style={{ display: 'flex' }}>
                    <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
                        {"Đăng ký nhân khẩu"}
                    </Typography>
                    <IconButton aria-label="close" onClick={() => {
                        setOpenPopup(!openPopup);
                    }}>
                        <CloseIcon></CloseIcon>
                    </IconButton>
                </div>
            </DialogTitle>
            <DialogContent dividers>
                <Box m="20px">
                    <Formik
                        onSubmit={handleFormSubmit}
                        initialValues={initialValues}
                        validationSchema={checkoutSchema}
                    >
                        {({
                            values,
                            errors,
                            touched,
                            handleBlur,
                            handleChange,
                            handleSubmit,
                        }) => (
                            <form onSubmit={handleSubmit}>
                                <Box
                                    display="grid"
                                    gap="30px"
                                    gridTemplateColumns="repeat(4, minmax(0, 1fr))"
                                    sx={{
                                        "& > div": { gridColumn: isNonMobile ? undefined : "span 4" },
                                    }}
                                >
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Họ tên"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.hoTen}
                                        name="hoTen"
                                        error={!!touched.hoTen && !!errors.hoTen}
                                        helperText={touched.hoTen && errors.hoTen}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 5" }}
                                    />
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Căn cước công dân"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.canCuocCongDan}
                                        name="canCuocCongDan"
                                        error={!!touched.canCuocCongDan && !!errors.canCuocCongDan}
                                        helperText={touched.canCuocCongDan && errors.canCuocCongDan}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 3" }}
                                    />
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Dân tộc"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.danToc}
                                        name="danToc"
                                        error={!!touched.danToc && !!errors.danToc}
                                        helperText={touched.danToc && errors.danToc}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 2" }}
                                    />
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Nghề nghiệp"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.ngheNghiep}
                                        name="ngheNghiep"
                                        error={!!touched.ngheNghiep && !!errors.ngheNghiep}
                                        helperText={touched.ngheNghiep && errors.ngheNghiep}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 3" }}
                                    />
                                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                                        <DesktopDatePicker label="Ngày sinh"
                                            inputFormat="DD/MM/YYYY"
                                            onChange={setDate}
                                            value={date}
                                            renderInput={(params) => <TextField {...params} />}>

                                        </DesktopDatePicker>
                                    </LocalizationProvider>
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Nơi sinh"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.noiSinh}
                                        name="noiSinh"
                                        error={!!touched.noiSinh && !!errors.noiSinh}
                                        helperText={touched.noiSinh && errors.noiSinh}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 5" }}
                                    />
                                    <TextField
                                        variant="filled"
                                        select
                                        label="Trạng thái"
                                        onBlur={handleBlur}
                                        name="trangThai"
                                        onChange={handleChange}
                                        defaultValue={values.trangThai}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 2" }}>
                                        <MenuItem value={0}>Đã mất</MenuItem>
                                        <MenuItem value={1}>Còn sống</MenuItem>
                                    </TextField>
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Quan hệ"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.quanHe}
                                        name="quanHe"
                                        error={!!touched.quanHe && !!errors.quanHe}
                                        helperText={touched.quanHe && errors.quanHe}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 3" }}
                                    />
                                    <TextField
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Ghi chú"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        value={values.ghiChu}
                                        name="ghiChu"
                                        error={!!touched.ghiChu && !!errors.ghiChu}
                                        helperText={touched.ghiChu && errors.ghiChu}
                                        sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 5" }}
                                    />
                                </Box>
                                <Box display="flex" justifyContent="end" mt="20px" >
                                    <Button
                                        type="submit" color="secondary" variant="contained" startIcon={<SaveAsIcon />}>
                                        Lưu
                                    </Button>
                                </Box>
                            </form>
                        )}
                    </Formik>
                </Box>
            </DialogContent>
        </Dialog>
    );
};
const checkoutSchema = yup.object().shape({
    hoTen: yup.string().required("Bạn chưa điền thông tin"),
    canCuocCongDan: yup.string().required("Bạn chưa điền thông tin").max(12, "Căn cước công dân không được quá 12 ký tự"),
    noiSinh: yup.string().required("Bạn chưa điền thông tin"),
    danToc: yup.string().required("Bạn chưa điền thông tin"),
    ngheNghiep: yup.string().required("Bạn chưa điền thông tin"),
    quanHe: yup.string().required("Bạn chưa điền thông tin"),
});
export default RegisterDemographic;


