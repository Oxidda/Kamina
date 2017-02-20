using Mcg.System;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;



namespace __InterfaceProxies
{
	public class ServiceChannelProxy_IUniversalTestExecutorService : global::System.ServiceModel.Channels.ServiceChannelProxy_P, global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P
	{
		global::Microsoft.VisualStudio.TestPlatform.Core.PollRequest_P global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.PollForRequest()
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[0];
			global::Microsoft.VisualStudio.TestPlatform.Core.PollRequest_P retval = ((global::Microsoft.VisualStudio.TestPlatform.Core.PollRequest_P)base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							));
			return retval;
		}

		void global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.TestConnection(global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> dummyTestResults)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					dummyTestResults
			};
			base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							);
		}

		void global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.OnTestRunChange(
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> newTestResults)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					testRunStats, 
					newTestResults
			};
			base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							);
		}

		void global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.OnTestRunChanges(
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> newTestResults, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase_P> inProgressTestCases)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					testRunStats, 
					newTestResults, 
					inProgressTestCases
			};
			base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							);
		}

		void global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.OnLogMessages(global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.Core.LogMessage_P> messages)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					messages
			};
			base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							);
		}

		void global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.OnRunComplete(
					bool isCanceled, 
					bool isAborted, 
					global::System.Collections.Generic.ICollection_P<string> executorUris, 
					global::System.Exception_P exception, 
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> lastChunk, 
					bool adapterHintedToShutdownAfterRun, 
					global::System.TimeSpan_P elapsedTime, 
					global::System.Collections.Generic.ICollection_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.AttachmentSet_P> runContextAttachments)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					isCanceled, 
					isAborted, 
					executorUris, 
					exception, 
					testRunStats, 
					lastChunk, 
					adapterHintedToShutdownAfterRun, 
					elapsedTime, 
					runContextAttachments
			};
			base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							);
		}

		int global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P.LaunchProcessWithDebuggerAttached(
					string filePath, 
					string workingDirectory, 
					string arguments, 
					global::System.Collections.Generic.IDictionary_P<string, string> environmentVariables)
		{
			global::System.RuntimeMethodHandle interfaceMethod = global::System.Reflection.DispatchProxyHelpers.GetCorrespondingInterfaceMethodFromMethodImpl();
			global::System.RuntimeTypeHandle interfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle;
			global::System.Reflection.MethodBase targetMethodInfo = global::System.Reflection.MethodBase.GetMethodFromHandle(
								interfaceMethod, 
								interfaceType
							);
			object[] callsiteArgs = new object[] {
					filePath, 
					workingDirectory, 
					arguments, 
					environmentVariables
			};
			int retval = ((int)base.Invoke(
								((global::System.Reflection.MethodInfo)targetMethodInfo), 
								callsiteArgs
							));
			return retval;
		}
	}

	[global::System.Runtime.CompilerServices.EagerStaticClassConstruction]
	[global::System.Runtime.CompilerServices.DependencyReductionRoot]
	public static class __DispatchProxyImplementationData
	{
		static global::System.Reflection.DispatchProxyEntry[] s_entryTable = new global::System.Reflection.DispatchProxyEntry[] {
				new global::System.Reflection.DispatchProxyEntry() {
					ProxyClassType = typeof(global::System.ServiceModel.Channels.ServiceChannelProxy_P).TypeHandle,
					InterfaceType = typeof(global::Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService_P).TypeHandle,
					ImplementationClassType = typeof(global::__InterfaceProxies.ServiceChannelProxy_IUniversalTestExecutorService).TypeHandle,
				}
		};
		static __DispatchProxyImplementationData()
		{
			global::System.Reflection.DispatchProxyHelpers.RegisterImplementations(s_entryTable);
		}
	}
}

namespace Microsoft.VisualStudio.TestPlatform.Core
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.Core.IUniversalTestExecutorService, Microsoft.VisualStudio.TestPlatform.Core" +
		", Version=15.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public interface IUniversalTestExecutorService_P
	{
		global::Microsoft.VisualStudio.TestPlatform.Core.PollRequest_P PollForRequest();

		void TestConnection(global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> dummyTestResults);

		void OnTestRunChange(
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> newTestResults);

		void OnTestRunChanges(
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> newTestResults, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase_P> inProgressTestCases);

		void OnLogMessages(global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.Core.LogMessage_P> messages);

		void OnRunComplete(
					bool isCanceled, 
					bool isAborted, 
					global::System.Collections.Generic.ICollection_P<string> executorUris, 
					global::System.Exception_P exception, 
					global::Microsoft.VisualStudio.TestPlatform.Core.TestRunStats_P testRunStats, 
					global::System.Collections.Generic.IEnumerable_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult_P> lastChunk, 
					bool adapterHintedToShutdownAfterRun, 
					global::System.TimeSpan_P elapsedTime, 
					global::System.Collections.Generic.ICollection_P<global::Microsoft.VisualStudio.TestPlatform.ObjectModel.AttachmentSet_P> runContextAttachments);

		int LaunchProcessWithDebuggerAttached(
					string filePath, 
					string workingDirectory, 
					string arguments, 
					global::System.Collections.Generic.IDictionary_P<string, string> environmentVariables);
	}
}

namespace System.Reflection
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.Reflection.DispatchProxy, System.Private.Interop, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f" +
		"7f11d50a3a")]
	public class DispatchProxy_P
	{
	}
}

namespace System.ServiceModel.Channels
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.ServiceModel.Channels.ServiceChannelProxy, System.Private.ServiceModel, Version=4.0.0.0, Culture=neutral," +
		" PublicKeyToken=b03f5f7f11d50a3a")]
	public class ServiceChannelProxy_P : global::System.Reflection.DispatchProxy
	{
		protected override object Invoke(
					global::System.Reflection.MethodInfo targetMethodInfo, 
					object[] args)
		{
			return null;
		}
	}
}

namespace Microsoft.VisualStudio.TestPlatform.Core
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.Core.PollRequest, Microsoft.VisualStudio.TestPlatform.Core, Version=15.0.0.0" +
		", Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class PollRequest_P
	{
	}
}

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.ObjectModel.TestResult, Microsoft.VisualStudio.TestPlatform.ObjectModel, Ver" +
		"sion=15.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TestResult_P
	{
	}
}

namespace System.Collections.Generic
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.Collections.Generic.IEnumerable`1, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyTok" +
		"en=b03f5f7f11d50a3a")]
	public interface IEnumerable_P<T>
	{
	}
}

namespace Microsoft.VisualStudio.TestPlatform.Core
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.Core.TestRunStats, Microsoft.VisualStudio.TestPlatform.Core, Version=15.0.0." +
		"0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TestRunStats_P
	{
	}
}

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase, Microsoft.VisualStudio.TestPlatform.ObjectModel, Versi" +
		"on=15.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TestCase_P
	{
	}
}

namespace Microsoft.VisualStudio.TestPlatform.Core
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.Core.LogMessage, Microsoft.VisualStudio.TestPlatform.Core, Version=15.0.0.0," +
		" Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class LogMessage_P
	{
	}
}

namespace System.Collections.Generic
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.Collections.Generic.ICollection`1, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyTok" +
		"en=b03f5f7f11d50a3a")]
	public interface ICollection_P<T>
	{
	}
}

namespace System
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.Exception, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Exception_P
	{
	}
}

namespace System
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.TimeSpan, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public struct TimeSpan_P
	{
	}
}

namespace Microsoft.VisualStudio.TestPlatform.ObjectModel
{
	[global::System.Runtime.InteropServices.McgRedirectedType("Microsoft.VisualStudio.TestPlatform.ObjectModel.AttachmentSet, Microsoft.VisualStudio.TestPlatform.ObjectModel, " +
		"Version=15.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class AttachmentSet_P
	{
	}
}

namespace System.Collections.Generic
{
	[global::System.Runtime.InteropServices.McgRedirectedType("System.Collections.Generic.IDictionary`2, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyTok" +
		"en=b03f5f7f11d50a3a")]
	public interface IDictionary_P<TKey, TValue>
	{
	}
}
