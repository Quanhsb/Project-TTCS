import { Box, Button, TextField, Typography, useTheme, Dialog, DialogTitle, DialogContent, IconButton } from "@mui/material";
import { tokens } from "../../theme";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import SaveAsIcon from '@mui/icons-material/SaveAs';
import DeleteSweepIcon from '@mui/icons-material/DeleteSweep';
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { fetchAllAbsents } from "../../Redux/absentSlice";
import CloseIcon from '@mui/icons-material/Close';
import absentService from "../../Services/API/absentService";
import { DesktopDatePicker, LocalizationProvider, } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const EditAbsent = ({ openInPopup, setOpenInPopup, data }) => {
  const theme = useTheme();
  const dispatch = useDispatch();
  const colors = tokens(theme.palette.mode);
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const [newDate, setNewDate] = useState(dayjs(data.thoiHan));
  const handleFormSubmit = (values) => {
    console.log(newDate);
    console.log(values);
    if (window.confirm("Bạn chắc chắn muốn lưu?") === true) {
      absentService.putAbsent(values.maTamVang, {
        maNhanKhau: values.maNhanKhau,
        thoiHan: newDate,
        lyDo: values.lyDo,
        version: data.version,
      }).then(mes => {
        toast(mes.message);
        setOpenInPopup(!openInPopup);
        dispatch(fetchAllAbsents());
      })
    }
  };
  const handleOnChange = (newValue) => {
    setNewDate(newValue);
  }
  useEffect(() => {
    console.log(data.thoiHan);
    setNewDate(data.thoiHan);
  }, [data.thoiHan]);
  const initialValues = {
    maNhanKhau: data.maNhanKhau,
    hoTen: data.hoTen,
    canCuocCongDan: data.canCuocCongDan,
    maTamVang: data.maTamVang,
    thoiHan: dayjs(data.thoiHan),
    lyDo: data.lyDo,
  };
  return (
    <Dialog open={openInPopup} maxWidth="md" style={{ backgroundColor: "transparent" }}>
      <DialogTitle>
        <div style={{ display: 'flex' }}>
          <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
            {"Chi tiết tạm vắng"}
          </Typography>
          <IconButton aria-label="close" onClick={() => {
            setOpenInPopup(!openInPopup)
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
                    width: 500
                  }}
                >
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Họ tên"
                    name="hoTen"
                    InputProps={{
                      readOnly: true,
                    }}
                    defaultValue={initialValues.hoTen}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Mã nhân khẩu"
                    name="maNhanKhau"
                    InputProps={{
                      readOnly: true,
                    }}
                    defaultValue={initialValues.maNhanKhau}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Căn cước công dân"
                    name="canCuocCongDan"
                    InputProps={{
                      readOnly: true,
                    }}
                    defaultValue={initialValues.canCuocCongDan}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Lý do"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.lyDo}
                    name="lyDo"
                    error={!!touched.lyDo && !!errors.lyDo}
                    helperText={touched.lyDo && errors.lyDo}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <DesktopDatePicker label="Ngày bắt đầu"
                      inputFormat="DD/MM/YYYY"
                      onChange={handleOnChange}
                      name="thoiHan"
                      value={newDate}
                      renderInput={(params) => <TextField style={{ width: 150 }} {...params} />}>
                    </DesktopDatePicker>
                  </LocalizationProvider>
                </Box>
                <Box display="flex" justifyContent="end" mt="20px" >
                  <Button onClick={() => {
                    if (window.confirm('Bạn thật sự muốn xóa?')) {
                      absentService.deleteAbsent(values.maTamVang, data.version).then(mes => {
                        toast(mes.message);
                        setOpenInPopup(!openInPopup);
                        dispatch(fetchAllAbsents());
                      })
                    }
                  }}
                    style={{ backgroundColor: colors.redAccent[600], marginRight: 10 }}
                    variant="contained" startIcon={<DeleteSweepIcon />}>Xóa
                  </Button>
                  <Button
                    type="submit" color="secondary" variant="contained" startIcon={<SaveAsIcon />}>
                    Lưu
                  </Button>
                </Box>
              </form>
            )}
          </Formik>
        </Box>
        <ToastContainer />
      </DialogContent>
    </Dialog>
  );
};
const checkoutSchema = yup.object().shape({
  lyDo: yup.string().required("Bạn chưa điền thông tin"),
});
export default EditAbsent;
