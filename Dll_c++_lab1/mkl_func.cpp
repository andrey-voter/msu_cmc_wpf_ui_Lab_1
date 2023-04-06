#include "pch.h"
#include "mkl.h"

extern "C"  _declspec(dllexport)
void Interpolate(MKL_INT number_x, MKL_INT number_y, double* x, double* y, double* bc, double* scoeff, MKL_INT nsite,
	double* site, MKL_INT ndorder, MKL_INT * dorder, double* result, int& ret, double* leftLim, double* rightLim, double* intRes)
{
	try
	{
		int status;
		DFTaskPtr task;
		status = dfdNewTask1D(&task, number_x, x, DF_NO_HINT, number_y, y, DF_MATRIX_STORAGE_ROWS);
		if (status != DF_STATUS_OK) { ret = -1; return; }
		status = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_2ND_LEFT_DER | DF_BC_2ND_RIGHT_DER, bc, DF_NO_IC, NULL, scoeff, DF_NO_HINT);
		if (status != DF_STATUS_OK) { ret = -1; return; }
		status = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
		if (status != DF_STATUS_OK) { ret = -1; return; }
		status = dfdInterpolate1D(task, DF_INTERP, DF_METHOD_PP, nsite, site, DF_UNIFORM_PARTITION, ndorder, dorder, NULL, result, DF_MATRIX_STORAGE_ROWS, NULL);
		if (status != DF_STATUS_OK) { ret = -1; return; }
		status = dfdIntegrate1D(task, DF_METHOD_PP, 1, leftLim, DF_NO_HINT, rightLim, DF_NO_HINT, NULL, NULL, intRes, DF_MATRIX_STORAGE_ROWS);
		if (status != DF_STATUS_OK) { ret = -1; return; }
		status = dfDeleteTask(&task);
		if (status != DF_STATUS_OK) { ret = -1; return; }

		ret = 0;
	}
	catch (...)
	{
		ret = -1;
	}
}