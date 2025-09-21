import { Stack, TextField } from "@mui/material";
import AdminLayout from "../../components/Layout/AdminLayout";
import { useContext, useEffect, useState } from "react";
import useInput from "../../hooks/useInput";
import AddIcon from "@mui/icons-material/Add";
import UploadButton from "../../components/UploadButton/UploadButton";
import {
  createProduct,
  deleteProduct,
  getProducts,
  updateProduct,
} from "../../api/productService";
import AuthContext from "../../store/AuthContext";
import DataGrid from "../../components/DataGrid/DataGrid";
import ProductRow from "../../components/DataGrid/ProdustRow/ProductRow";
import CreateButton from "../../components/CreateButton/CreateButton";
import UpdateButton from "../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";

const errMsg = "Ошибка ввода";

const validateNumber = (num) =>
  num > 0 ? { isValid: true, err: null } : { isValid: false, err: errMsg };

const gridHeaders = [
  { key: "name", value: "Название" },
  { key: "calories", value: "Калории" },
  { key: "proteins", value: "Белки" },
  { key: "fats", value: "Жиры" },
  { key: "carbohydrates", value: "Углеводы" },
  { key: "imageUrl", value: "Картинка", isUnSortable: true },
  { key: "edit", value: "", isUnSortable: true },
];

const ProductPage = () => {
  const { token } = useContext(AuthContext);
  const product = useInput("Продукт1", (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const calories = useInput("1", validateNumber);
  const proteins = useInput("1", validateNumber);
  const fats = useInput("1", validateNumber);
  const carbo = useInput("1", validateNumber);
  const file = useInput(null, (file) =>
    file.type.split("/")[0] !== "image"
      ? { isValid: false, err: "Нужна картинка" }
      : { isValid: true, err: null }
  );
  const [fetchedProducts, setFetchedProducts] = useState([]);
  const [productInEdit, setProductInEdit] = useState(null);

  const get = async () => {
    try {
      const data = await getProducts(token);
      setFetchedProducts(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    get();
  }, []);

  useEffect(() => {
    if (!productInEdit) return;

    product.setValue(productInEdit.name);
    calories.setValue(productInEdit.calories);
    proteins.setValue(productInEdit.proteins);
    fats.setValue(productInEdit.fats);
    carbo.setValue(productInEdit.carbohydrates);
  }, [productInEdit]);

  const createFormData = () => {
    const formData = new FormData();

    formData.append("name", product.value);
    formData.append("calories", calories.value);
    formData.append("fats", fats.value);
    formData.append("proteins", proteins.value);
    formData.append("carbohydrates", carbo.value);
    if (file) {
      formData.append("image", file.value);
    }

    return formData;
  };

  const resetInputs = () => {
    product.reset();
    calories.reset();
    proteins.reset();
    fats.reset();
    carbo.reset();
    file.reset();
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = createFormData();

    try {
      if (productInEdit) {
        formData.append("id", productInEdit.id);
        await updateProduct(token, formData);
        setProductInEdit(null);
      } else {
        await createProduct(token, formData);
      }
    } catch (err) {
      console.log(err);
    } finally {
      resetInputs();
    }

    await get();
  };

  const handleEdit = async (id) => {
    setProductInEdit(fetchedProducts.find((product) => product.id === id));
  };

  const handleDelete = async (id) => {
    try {
      await deleteProduct(token, id);
    } catch (err) {
      console.log(err);
    }

    await get();
  };

  return (
    <AdminLayout>
      <form onSubmit={handleSubmit} style={{ height: 70 }}>
        <Stack direction="row" sx={{ gap: 2 }}>
          <TextField
            label="Название"
            value={product.value}
            onChange={product.onchange}
            error={product.error !== null}
            helperText={product.error}
          />
          <TextField
            label="Калории"
            type="number"
            sx={{ maxWidth: 120 }}
            value={calories.value}
            onChange={calories.onchange}
            error={calories.error !== null}
            helperText={calories.error}
          />
          <TextField
            label="Белки"
            type="number"
            sx={{ maxWidth: 120 }}
            value={proteins.value}
            onChange={proteins.onchange}
            error={proteins.error !== null}
            helperText={proteins.error}
          />
          <TextField
            label="Жиры"
            type="number"
            sx={{ maxWidth: 120 }}
            value={fats.value}
            onChange={fats.onchange}
            error={fats.error !== null}
            helperText={fats.error}
          />
          <TextField
            label="Углеводы"
            type="number"
            sx={{ maxWidth: 120 }}
            value={carbo.value}
            onChange={carbo.onchange}
            error={carbo.error !== null}
            helperText={carbo.error}
          />
          <UploadButton
            fileName={file.value && file.value.name}
            onChange={file.onchange}
            reset={file.reset}
          >
            Файл
          </UploadButton>
          {productInEdit ? (
            <UpdateButton
              sx={{ marginLeft: "auto", marginRight: 4 }}
              type="submit"
            >
              <EditOutlinedIcon />
            </UpdateButton>
          ) : (
            <CreateButton
              sx={{ marginLeft: "auto", marginRight: 4 }}
              type="submit"
            >
              <AddIcon />
            </CreateButton>
          )}
        </Stack>
      </form>
      <DataGrid
        headers={gridHeaders}
        minCellWidth={50}
        data={fetchedProducts}
        component={ProductRow}
        onEditClick={handleEdit}
        onDeleteClick={handleDelete}
        rowInEdit={productInEdit}
      ></DataGrid>
    </AdminLayout>
  );
};

export default ProductPage;
