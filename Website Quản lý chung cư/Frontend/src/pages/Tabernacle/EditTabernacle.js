import { Box, Button, TextField, Typography, useTheme, Dialog, DialogTitle, DialogContent, IconButton, MenuItem } from "@mui/material";
import { tokens } from "../../theme";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import SaveAsIcon from '@mui/icons-material/SaveAs';
import DeleteSweepIcon from '@mui/icons-material/DeleteSweep';
import { useEffect, useState } from "react";
import CloseIcon from '@mui/icons-material/Close';
import { fetchAllTabernacles } from "../../Redux/tabernacleSlice";
import { useDispatch } from "react-redux";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
const EditTabernacle = ({ openInPopup, setOpenInPopup, data }) => {
  const [dataPhong, setDataPhong] = useState([]);
  const [roomName, setRoomName] = useState("");
  const dispatch = useDispatch();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const handleGetData = async () => {
    roomService.getListRoom().then((result) => {
      let datas = result.data;
      console.log(datas);
      const datamap = datas.map((data) => {
        const label = data.tenCanHo;
        return {
          label,
          value: data.maCanHo.toString()
        }
      })
      console.log('datamap canHo', datamap);
      setDataPhong(datamap);
    }).catch(e => {
      console.log(e);
    })
  }
  const handleFormSubmit = (values) => {
    console.log('roomName', roomName);
    if (roomName && window.confirm("Bạn chắc chắn muốn lưu?")) {
      tabernacleService.putTabernacle(data.maTamTru, {
        hoTen: values.hoTen,
        diaChiThuongTru: values.diaChiThuongTru,
        diaChiTamTru: roomName.toString(),
        canCuocCongDan: values.canCuocCongDan,
        version: data.version,
      }).then(mes => {
        toast(mes.message);
        setOpenInPopup(!openInPopup);
        dispatch(fetchAllTabernacles());
      }).catch(e => {
        if (e.response.data.reason)
          toast(e.response.data.reason)
        else toast(e.response.data.message)
      })
    }
  };
  const initialValues = {
    hoTen: data.hoTen,
    maTamTru: data.maTamTru,
    canCuocCongDan: data.canCuocCongDan,
    diaChiThuongTru: data.diaChiThuongTru,
  };
  useEffect(() => {
    setRoomName(data.diaChiTamTru)
  }, [data])
  useEffect(() => {
    handleGetData()
  }, [])
  return (
    <Dialog open={openInPopup} maxWidth="md" style={{ backgroundColor: "transparent" }}>
      <DialogTitle>
        <div style={{ display: 'flex' }}>
          <Typography variant="h6" component="div" style={{ flexGrow: 1, fontSize: 20, fontWeight: "bold" }}>
            {"Chi tiết tạm trú"}
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
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.hoTen}
                    name="hoTen"
                    error={!!touched.hoTen && !!errors.hoTen}
                    helperText={touched.hoTen && errors.hoTen}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
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
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <TextField
                    fullWidth
                    variant="filled"
                    type="text"
                    label="Địa chỉ thường trú"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.diaChiThuongTru}
                    name="diaChiThuongTru"
                    error={!!touched.diaChiThuongTru && !!errors.diaChiThuongTru}
                    helperText={touched.diaChiThuongTru && errors.diaChiThuongTru}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}
                  />
                  <TextField
                    variant="filled"
                    select
                    label="Địa chỉ tạm trú"
                    onBlur={handleBlur}
                    name="diaChiTamTru"
                    onChange={(e) => setRoomName(e.target.value)}
                    value={roomName}
                    error={!!touched.diaChiThuongTru && !roomName}
                    helperText={touched.diaChiThuongTru && !roomName && "Bạn chưa điền thông tin"}
                    sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}>
                    {dataPhong.map((canHo, index) => {
                      return <MenuItem key={index} value={canHo.label}>{canHo.label}</MenuItem>
                    })}
                  </TextField>
                </Box>
                <Box display="flex" justifyContent="end" mt="20px" >
                  <Button onClick={() => {
                    if (window.confirm("Bạn thật sự muốn xóa?")) {
                      tabernacleService.deleteTabernacle(data.maTamTru, data.version).then(mes => {
                        toast(mes.message);
                        setOpenInPopup(!openInPopup);
                        dispatch(fetchAllTabernacles());
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
      </DialogContent>
    </Dialog>
  );
};
const checkoutSchema = yup.object().shape({
  hoTen: yup.string().required("Bạn chưa điền thông tin"),
  canCuocCongDan: yup
    .string().required("Bạn chưa điền thông tin").max(12, "Căn cước công dân không được quá 12 ký tự"),
  diaChiThuongTru: yup.string().required("Bạn chưa điền thông tin"),
});
export default EditTabernacle;
