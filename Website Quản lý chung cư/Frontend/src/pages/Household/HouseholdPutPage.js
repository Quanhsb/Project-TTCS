import { Box, Button, MenuItem, TextField } from "@mui/material";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import Header from "../../components/Header";
import CustomSelect from "../../components/CustomSelect";
import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "../../setups/custom_axios";
import Select from 'react-select';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import { useHistory } from 'react-router-dom';
import SaveAsIcon from '@mui/icons-material/SaveAs';
import dayjs from "dayjs";
import householdService from "../../Services/API/householdService";
import roomService from "../../Services/API/roomService";
import RegisterXe from "./RegisterXe";
import EditXe from "./EditXe";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ModalDetailDemographic from "./ModalDetailDemographic";
import { DesktopDatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
const HouseholdAddPage = () => {
  const history = useHistory();
  const isNonMobile = useMediaQuery("(min-width:600px)");
  const { id } = useParams();
  const [detailHouseholdData, setdetailHouseHoldData] = useState({})
  const [isLoading, setIsLoading] = useState(true);
  const [selectOption, setSelectOption] = useState([]);
  const [roomId, setRoomId] = useState("-1");
  const [xeArr, setXeArr] = useState([]);
  const [showDetailMember, setShowDetailMember] = useState(false);
  const [addXe, setAddXe] = useState(false);
  const [editXe, setEditXe] = useState(false);
  const [ngayCap, setNgayCap] = useState();
  const [currentXe, setCurrentXe] = useState({});
  const handleSelect = (selectedOption) => {
    setSelectOption(selectedOption);
  };
  const handleUpdate = (values) => {
    console.log('values', values);
    if (window.confirm("Bạn chắc chắn muốn lưu?") === true) {
      householdService.addRoomToHouseHold(values.maHoKhau, roomId).then((result) => {
        console.log('result', result);
      }).catch(e => {
        toast(e?.response?.data?.message);
      })
      console.log('selectOption', selectOption);
      console.log('ngayCap', ngayCap);
      householdService.updateHouseHold(values.maHoKhau, {
        "diaChiThuongTru": values.diaChiThuongTru,
        "noiCap": values.noiCap,
        "ngayCap": dayjs(ngayCap).format('YYYY-MM-DD'),
        "danhSachNhanKhau": selectOption.map((nhankhau) => {
          return nhankhau.value;
        }),
        "version": initialValues.version,
      }).then((result) => {
        toast(result.message);
        handleGetData();
      }).catch(e =>
        toast(e?.response?.data?.message))
    }
  }
  const [initialValues, setInitialValues] = useState({});
  const handleGetData = async () => {
    try {
      const response = await axios.get(`/nhan-khau/danh-sach-nhan-khau-chua-co-ho-khau`)
      const datas = response.data;
      const datamap = datas.map((data) => {
        const label = `${data.hoTen} ${data.maNhanKhau}`
        return {
          label,
          value: data.maNhanKhau
        }
      })
      console.log('datamap', datamap);
      setDataNhanKhau(datamap);
    } catch (error) {
      console.log(error);
    }
    try {
      setIsLoading(true);
      const response = await axios.get(`ho-khau?maHoKhau=${id}`)
      setdetailHouseHoldData(response.data);
      console.log(response.data);
      setRoomId(response.data.maCanHo.toString());
      setXeArr(response.data.danhSachXe);
      const datamapNhanKhau = response.data.danhSachNhanKhau.map((data) => {
        const label = data.hoTen + " " + data.maNhanKhau
        return {
          label,
          value: data.maNhanKhau
        }
      })
      setSelectOption(datamapNhanKhau);
      setNgayCap(response.data.ngayCap);
      setInitialValues({
        maHoKhau: response.data.maHoKhau,
        diaChiThuongTru: response.data.diaChiThuongTru,
        noiCap: response.data.noiCap,
        danhSachNhanKhau: datamapNhanKhau,
        maCanHo: response.data.maCanHo,
        version: response.data.version,
      })
      setIsLoading(false);
    } catch (error) {
      history.push('/household');
    }
    roomService.getListRoom().then((result) => {
      let datas = result.data;
      console.log(datas);
      datas = datas.filter(o => !o.maHoKhau || o.maHoKhau === id)
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
  const [dataNhanKhau, setDataNhanKhau] = useState([]);
  const [dataPhong, setDataPhong] = useState([]);
  const handleRemoveXe = (maXe) => {
    let text = "Bạn chắc chắn muốn xóa?";
    if (!window.confirm(text)) {
      return
    }
    householdService.removeXe(maXe).then((result) => {
      toast(result.message);
      handleGetData();
    }).catch(e => {
      toast(e?.response?.data?.message);
    })
  }
  useEffect(() => {
    handleGetData();
  }, []);
  console.log('roomId', roomId);
  if (isLoading) {
    return <div>Loading....</div>
  }
  return (
    <Box m="20px" onLoad>
      <Header title="Cập nhật hộ khẩu" />
      {addXe && <RegisterXe maHoKhau={initialValues.maHoKhau} onClose={() => setAddXe(false)} onSuccess={() => handleGetData()} />}
      {editXe && <EditXe xeData={currentXe} onClose={() => setEditXe(false)} onSuccess={() => handleGetData()} />}
      <Formik
        onSubmit={(values) => {
          handleUpdate(values)
        }}
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
            <div style={{ display: 'flex', flexDirection: 'row' }}>
              <Box
                display="grid"
                gap="30px"
                gridTemplateColumns="repeat(4, minmax(0, 1fr))"
                sx={{
                  "& > div": { gridColumn: isNonMobile ? undefined : "span 4" },
                  width: '100%'
                }}
              >
                <TextField
                  fullWidth
                  variant="filled"
                  type="text"
                  label="Mã hộ khẩu"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={id}
                  name="maHoKhau"
                  error={!!touched.maHoKhau && !!errors.maHoKhau}
                  helperText={touched.maHoKhau && errors.maHoKhau}
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
                  fullWidth
                  variant="filled"
                  type="text"
                  label="Nơi cấp"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.noiCap}
                  name="noiCap"
                  error={!!touched.noiCap && !!errors.noiCap}
                  helperText={touched.noiCap && errors.noiCap}
                  sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 2" }}
                />
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <DesktopDatePicker label="Ngày cấp"
                    inputFormat="DD/MM/YYYY"
                    onChange={setNgayCap}
                    value={ngayCap}
                    renderInput={(params) => <TextField {...params} />}>
                  </DesktopDatePicker>
                </LocalizationProvider>
                <TextField
                  variant="filled"
                  select
                  label="Căn hộ"
                  onBlur={handleBlur}
                  name="canHo"
                  onChange={(e) => setRoomId(+e.target.value)}
                  value={roomId}
                  sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, gridColumn: "span 4" }}>
                  <MenuItem value={"-1"}>None</MenuItem>
                  {dataPhong.map((canHo, index) => {
                    return <MenuItem key={index} value={canHo.value}>{canHo.label}</MenuItem>
                  })}
                </TextField>
                <div style={{ display: 'flex', flexDirection: 'column' }}></div>
              </Box>
              <Box>
                <div style={{ width: '60vh', marginBottom: 10, marginLeft: 10, marginTop: -32 }}>
                  <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                    <Button onClick={() => setAddXe(true)}
                      size="small" variant="contained" color="info" style={{ marginBottom: 5, color: 'white' }}>Thêm xe</Button>
                  </div>
                  <table className="custom-table">
                    <thead>
                      <tr>
                        <th>Mã xe</th>
                        <th>Tên xe</th>
                        <th>Biển kiểm soát</th>
                        <th>Loại xe</th>
                        <th></th>
                      </tr>
                    </thead>
                    <tbody>
                      {xeArr.map((row, index) => {
                        return <tr key={index}>
                          <td>{row.maXe}</td>
                          <td>{row.tenXe}</td>
                          <td>{row.bienKiemSoat}</td>
                          <td>{row.maLoaiXe === "LX001" ? "Xe máy" : "Xe ô tô"}</td>
                          <td>
                            <div style={{
                              display: 'flex',
                              flexDirection: 'row',
                              justifyContent: 'space-evenly'
                            }}>
                              <EditIcon onClick={() => { setCurrentXe(row); setEditXe(true) }} style={{ cursor: 'pointer' }} />
                              <DeleteIcon onClick={() => handleRemoveXe(row.maXe)}
                                style={{ cursor: 'pointer' }} />
                            </div>
                          </td>
                        </tr>
                      })}
                    </tbody>
                  </table>
                </div>
              </Box>
            </div>
            <div style={{ display: 'flex', flexDirection: 'row' }}>
              <div style={{ width: '500px', marginTop: 0 }}>
                <Select
                  name="danhSachNhanKhau"
                  options={dataNhanKhau.concat(values.danhSachNhanKhau)}
                  component={CustomSelect}
                  placeholder="Danh sách mã nhân khẩu "
                  isMulti={true}
                  defaultValue={values.danhSachNhanKhau}
                  onChange={handleSelect}
                />
              </div>
              <div style={{ marginTop: 9, marginLeft: 10 }}>
                <VisibilityIcon style={{ cursor: 'pointer' }} onClick={() => setShowDetailMember(true)} />
              </div>
            </div>
            <Box display="flex" justifyContent="end" mt="20px">
              <Button startIcon={<SaveAsIcon />}
                type="submit" color="info" variant="contained" onClick={() => console.log(values.maCanHo)}>
                Lưu
              </Button>
            </Box>
          </form>
        )}
      </Formik>
      {showDetailMember && <ModalDetailDemographic memberData={detailHouseholdData?.danhSachNhanKhau} onClose={() => setShowDetailMember(false)} />}
    </Box>
  );
};
const checkoutSchema = yup.object().shape({
  maHoKhau: yup.string().required("Bạn chưa điền thông tin"),
  diaChiThuongTru: yup.string().required("Bạn chưa điền thông tin"),
  noiCap: yup.string().required("Bạn chưa điền thông tin"),
  danhSachNhanKhau: yup.array().required("Bạn chưa điền thông tin")
});
export default HouseholdAddPage;
