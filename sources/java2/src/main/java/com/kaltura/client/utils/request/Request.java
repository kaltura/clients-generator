package com.kaltura.client.utils.request;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Proxy;

import com.kaltura.client.Files;
import com.kaltura.client.Params;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.ObjectBase;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;


public class Request<T, V> extends RequestBuilder<T> {
	protected String id;

    public Request(Class<T> type, String service, String action, Params params, Files files) {
        super(type, service, action, params, files);
    }

    public Request(Class<T> type, String service, String action, Params params) {
        this(type, service, action, params, null);
    }

    public static class Tokenizer implements InvocationHandler {
    	private String prefix;
    	
    	public Tokenizer(String prefix) {
    		this.prefix = prefix;
    	}

		@Override
		public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
			String token = prefix + ":" + method.getName();

			if(method.getReturnType().isInterface()) {
				return getTokenizer(method.getReturnType(), token);
			}
			else if(ListTokenizer.class.isAssignableFrom(method.getReturnType())) {
				ParameterizedType genericReturnType = (ParameterizedType) method.getGenericReturnType();
				Class<?> intrface = (Class<?>) genericReturnType.getActualTypeArguments()[0];
				return ListTokenizer.newInstance(intrface, token);
			}
			else if(MapTokenizer.class.isAssignableFrom(method.getReturnType())) {
				ParameterizedType genericReturnType = (ParameterizedType) method.getGenericReturnType();
				Class<?> intrface = (Class<?>) genericReturnType.getActualTypeArguments()[0];
				return MapTokenizer.newInstance(intrface, token);
			}
			else {
				return "{" + token + "}";
			}
		}
    }
    
    public static class ListResponseTokenizer<I> implements ListResponse.Tokenizer<I> {
    	private Class<I> intrface;
    	private String prefix;
    	
    	public ListResponseTokenizer(Class<I> intrface, String prefix) {
    		this.intrface = intrface;
    		this.prefix = prefix;
    	}

		public String totalCount() {
			return "{" + prefix + ":totalCount}";
		}
		
		public ListTokenizer<I> objects() {
			return ListTokenizer.newInstance(intrface, prefix + ":objects");
		}
    }
    
    public static class ListTokenizer<I> {
    	private Class<I> intrface;
    	private String prefix;
    	
    	public static <J> ListTokenizer<J> newInstance(Class<J> intrface, String prefix) {
    		return new ListTokenizer<J>(intrface, prefix);
    	}
    	
    	public ListTokenizer(Class<I> intrface, String prefix) {
    		this.intrface = intrface;
    		this.prefix = prefix;
    	}
    	
		public I get(int index) {
			return getTokenizer(intrface, prefix + ":" + index);
		}
    }
    
    public static class MapTokenizer<I> {
    	private Class<I> intrface;
    	private String prefix;

    	public static <J> MapTokenizer<J> newInstance(Class<J> intrface, String prefix) {
    		return new MapTokenizer<J>(intrface, prefix);
    	}
    	
    	public MapTokenizer(Class<I> intrface, String prefix) {
    		this.intrface = intrface;
    		this.prefix = prefix;
    	}
    	
		public I get(String key) {
			return getTokenizer(intrface, prefix + ":" + key);
		}
    	
		public <J extends I> J get(String key, Class<J> tokenizerInterface) {
			return getTokenizer(tokenizerInterface, prefix + ":" + key);
		}
    }
    
    @SuppressWarnings("unchecked")
    static protected <I> I getTokenizer(Class<I> intrface, String prefix) {
    	Class<?>[] parentInterfaces = intrface.getInterfaces();
    	Class<?>[] interfaces = new Class<?>[parentInterfaces.length + 1];
    	for(int i = 0; i <  parentInterfaces.length; i++) {
    		interfaces[i] = parentInterfaces[i];
    	}
    	interfaces[parentInterfaces.length] = intrface;
        return (I) Proxy.newProxyInstance(intrface.getClassLoader(), interfaces, new Tokenizer(prefix));
    }
    
    @SuppressWarnings("unchecked")
	public V getTokenizer() throws APIException {
		if(id == null) {
			throw new APIException(APIException.FailureStep.OnRequest, "Request is not part of multi-request");
		}
		
    	if(ObjectBase.class.isAssignableFrom(type)) {
        	MultiRequestBuilder.Tokenizer annotation = type.getAnnotation(MultiRequestBuilder.Tokenizer.class);
        	Class<?>[] parentInterfaces = annotation.value().getInterfaces();
        	Class<?>[] interfaces = new Class<?>[parentInterfaces.length + 1];
        	for(int i = 0; i <  parentInterfaces.length; i++) {
        		interfaces[i] = parentInterfaces[i];
        	}
        	interfaces[parentInterfaces.length] = annotation.value();
            return (V) Proxy.newProxyInstance(type.getClassLoader(), interfaces, new Tokenizer(id + ":result"));
    	}
    	else {
    		return (V) ("{" + id + ":result}");
    	}
    }
    
    public MultiRequestBuilder add(Request<?, ?> another) {
        try {
            return new MultiRequestBuilder(this, another);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new MultiRequestBuilder();
    }
    
    protected String getAction() {
        return action;
    }

    protected Params getParams() {
        return params;
    }

    protected String getService() {
        return service;
    }

    protected Request<T, V> link(String destKey, String requestId, String sourceKey) {
        params.link(destKey, requestId, sourceKey);
        return this;
    }

    protected Request<T, V> setId(String id) {
        this.id = id;
        return this;
    }

    protected String getId() {
        return id;
    }

    public Request<T, V> setCompletion(OnCompletion<Response<T>> onCompletion) {
        super.setCompletion(onCompletion);
        return this;
    }
}














