// Desc: Login validation
const rules = {
  userNameOrEmailAddress: [
    {
      required: true,
      message: "Vui lòng điền đầy đủ thông tin",
    },
  ],
  password: [{ required: true, message: "Vui lòng điền đầy đủ thông tin" }],
};

export default rules;
